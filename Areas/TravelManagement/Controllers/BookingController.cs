using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.Data;
using GBC_Travel_Group_90.Filters;
using GBC_Travel_Group_90.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Controllers
{
    [Area("TravelManagement")]
    [Route("[area]/[controller]/[action]")]
    [ServiceFilter(typeof(LoggingFilter))]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookingController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("BookFlight/{id:int}")]
        public async Task<IActionResult> BookFlight(int id)
        {
            var flight = await _db.Flights.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        [HttpPost("BookFlight/{id:int}")]
        public async Task<IActionResult> BookFlight(string email, int id)
        {

            var flight = await _db.Flights.FirstOrDefaultAsync(f => f.FlightId == id);

            if (flight == null)
            {
                return NotFound();
            }

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    FirstName = "Guest",
                    LastName = "Guest"
                };
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
            }

            // Check if the user already booked flight
            var existingBooking = await _db.Bookings.FirstOrDefaultAsync(b => b.UserId == user.UserId && b.FlightId == id);

            if (existingBooking != null)
            {
                return View("AlreadyBooked");
            }

            var booking = new Booking
            {
                User = user,
                UserId = user.UserId,
                FlightId = id,
                Flight = flight
            };

            await _db.Bookings.AddAsync(booking);
            flight.CurrentPassengers++;
            await _db.SaveChangesAsync();
            return RedirectToAction("Success", new { id = booking.BookingId });

        }

        [HttpGet("SuccessBooking/{id:int}")]
        public async Task<IActionResult> Success(int id)
        {
            // Retrieve the booking from the database
            var booking = await _db.Bookings.Include(b => b.User)
                                      .Include(b => b.Flight)
                                      .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }
    }

}
