using GBC_Travel_Group_90.Data;
using GBC_Travel_Group_90.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_90.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookingController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult BookFlight(int id)
        {
            var flight = _db.Flights.Find(id);

            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        [HttpPost]
        public IActionResult BookFlight(string email, int id)
        {

            var flight = _db.Flights.FirstOrDefault(f => f.FlightId == id);

            if (flight == null)
            {
                return NotFound();
            }

            var user = _db.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    FirstName = "Guest",
                    LastName = "Guest"
                };
                _db.Users.Add(user);
                _db.SaveChanges();
            }

            // Check if the user already booked flight
            var existingBooking = _db.Bookings.FirstOrDefault(b => b.UserId == user.UserId && b.FlightId == id);

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

            _db.Bookings.Add(booking);
            flight.CurrentPassengers++;
            _db.SaveChanges();
            return RedirectToAction("Success", new { id = booking.BookingId });

        }

        [HttpGet]
        public IActionResult Success(int id)
        {
            // Retrieve the booking from the database
            var booking = _db.Bookings.Include(b => b.User)
                                      .Include(b => b.Flight)
                                      .FirstOrDefault(b => b.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }
    }

}
