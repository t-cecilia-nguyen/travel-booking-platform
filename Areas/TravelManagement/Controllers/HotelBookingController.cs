using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_90.Data;
using GBC_Travel_Group_90.Models;
using Microsoft.CodeAnalysis;
using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.Filters;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Controllers
{
    [Area("TravelManagement")]
    [Route("[area]/[controller]/[action]")]
    public class HotelBookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelBookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HotelBookings
        public async Task<IActionResult> Index(string email, bool isAdmin = false)
        {
            ViewBag.IsAdmin = false;
            if (isAdmin)
            {
                ViewBag.IsAdmin = true;
            }

            var applicationDbContext = _context.HotelBookings.Include(h => h.Hotel).Include(h => h.User);

            if (email == null || string.IsNullOrEmpty(email))
            {
                ViewBag.IsAdmin = false;

                return View(await applicationDbContext.ToListAsync());
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.IsAdmin);
            if (user != null)
            {
                Console.WriteLine("admin is true");
                ViewBag.IsAdmin = true;
            }

            return View(await applicationDbContext.ToListAsync());



        }

        // GET: HotelBookings/Details/5
        public async Task<IActionResult> Details(int? id, bool isAdmin = false)
        {
            ViewBag.IsAdmin = false;

            if (isAdmin)
            {
                ViewBag.IsAdmin = true;
            }

            if (id == null)
            {
                return NotFound();
            }

            var hotelBooking = await _context.HotelBookings
                .Include(h => h.Hotel)
                .Include(h => h.User)
                .FirstOrDefaultAsync(m => m.HotelBookingId == id);
            if (hotelBooking == null)
            {
                return NotFound();
            }

            return View(hotelBooking);
        }


        private async Task<bool> IsRoomAvailable(int? hotelId, int numOfRoomsToBook)
        {
            var hotel = await _context.Hotels.FindAsync(hotelId);

            if (hotel == null || numOfRoomsToBook <= 0)
            {
                // Hotel not found or invalid number of rooms to book
                return false;
            }

            var numOfRooms = hotel.NumberOfRooms;

            return numOfRooms >= numOfRoomsToBook;
        }

        private void UpdateNumberOfRooms(int hotelId, int numOfRoomsToBook)
        {
            var hotel = _context.Hotels.Find(hotelId);

            if (hotel != null && numOfRoomsToBook > 0)
            {
                hotel.NumberOfRooms -= numOfRoomsToBook;
                _context.SaveChanges();
            }
        }
        private bool IsValidBookingDates(DateTime checkInDate, DateTime checkOutDate)
        {
            // Ensure that CheckInDate is smaller than or equal to CheckOutDate
            return checkInDate <= checkOutDate;
        }

        [ServiceFilter(typeof(LoggingFilter))]
        // GET: HotelBookings/Create
        public async Task<IActionResult> Create(int hotelId, bool isAdmin = false)
        {
            ViewBag.IsAdmin = false;
            if (isAdmin)
            {
                ViewBag.IsAdmin = true;
            }

            var hotel = await _context.Hotels.FindAsync(hotelId);
            if (hotel == null) return NotFound("Hotel not found");


            var hotelBooking = new HotelBooking { HotelId = hotelId, Status = Status.Pending };

            ViewBag.HotelId = hotelId;


            return View(hotelBooking);
        }
        [ServiceFilter(typeof(LoggingFilter))]
        // POST: HotelBookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> Create([Bind("HotelBookingId, CheckInDate,CheckOutDate, NumOfRoomsToBook,HotelId")] HotelBooking hotelBooking, string userEmail)
        {
            if (ModelState.IsValid)
            {

                // Check if the user already booked the hotel
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
                if (user == null)
                {
                    user = new User
                    {
                        Email = userEmail,
                        FirstName = "Guest",
                        LastName = "Guest"
                    };
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                }

                var existingBooking = await _context.HotelBookings.FirstOrDefaultAsync(b => b.UserId == user.UserId && b.HotelId == hotelBooking.HotelId);

                if (existingBooking != null)
                {
                    TempData["AlreadyBooked"] = "You have already booked this hotel.";
                    return RedirectToAction("Index", "Hotel", new { email = userEmail });
                }

                // Check if CheckInDate is smaller than or equal to CheckOutDate
                if (!IsValidBookingDates(hotelBooking.CheckInDate, hotelBooking.CheckOutDate))
                {
                    ModelState.AddModelError(nameof(hotelBooking.CheckInDate), "Check-in date must be smaller than or equal to check-out date.");
                    return View(hotelBooking);
                }

                // Check if rooms are available
                if (await IsRoomAvailable(hotelBooking.HotelId, hotelBooking.NumOfRoomsToBook))
                {
                    // Update the NumberOfRooms property
                    UpdateNumberOfRooms(hotelBooking.HotelId, hotelBooking.NumOfRoomsToBook);


                    // Continue booking 

                    hotelBooking.UserId = user.UserId;
                    hotelBooking.BookingDate = DateTime.Now;
                    hotelBooking.Status = Status.Confirmed;
                    await _context.HotelBookings.AddAsync(hotelBooking);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Details), new { id = hotelBooking.HotelBookingId });
                }
                else
                {
                    // Display error message to the user
                    ModelState.AddModelError(nameof(hotelBooking.NumOfRoomsToBook), "Not enough rooms available for booking.");

                    ViewBag.HotelId = hotelBooking.HotelId;
                    ViewBag.UserId = hotelBooking.UserId;

                    return View(hotelBooking);
                }
            }
            return View(hotelBooking);
        }

        // GET: HotelBookings/Edit/5
        public async Task<IActionResult> Edit(int? id, bool isAdmin = false)
        {

            if (isAdmin)
            {
                ViewBag.IsAdmin = true;
            }

            if (id == null)
            {
                return NotFound();
            }

            var hotelBooking = await _context.HotelBookings.FindAsync(id);
            if (hotelBooking == null)
            {
                return NotFound();
            }


            return View(hotelBooking);
        }

        // POST: HotelBookings/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(ValidateModelFilter))]

        public async Task<IActionResult> Edit(int id, [Bind("HotelBookingId, NumOfRoomsToBook, CheckInDate,CheckOutDate,UserId, HotelId")] HotelBooking hotelBooking)
        {
            if (id != hotelBooking.HotelBookingId)
            {
                Response.StatusCode = 404;
                return View("Error/", new { statusCode = 404});
            }

            if (ModelState.IsValid)
            {
                try
                {
                    hotelBooking.BookingDate = DateTime.Now;
                    hotelBooking.Status = Status.Confirmed;
                    _context.Update(hotelBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await HotelBookingExists(hotelBooking.HotelBookingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = hotelBooking.HotelBookingId });
            }
            return View(hotelBooking);
        }

        // GET: HotelBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelBooking = await _context.HotelBookings
                .Include(h => h.Hotel)
                .Include(h => h.User)
                .FirstOrDefaultAsync(m => m.HotelBookingId == id);
            if (hotelBooking == null)
            {
                return NotFound();
            }

            return View(hotelBooking);
        }

        // POST: HotelBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotelBooking = await _context.HotelBookings.FindAsync(id);
            if (hotelBooking != null)
            {
                _context.HotelBookings.Remove(hotelBooking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Hotel");
        }

        private async Task<bool> HotelBookingExists(int id)
        {
            return await _context.HotelBookings.AnyAsync(e => e.HotelBookingId == id);
        }
    }
}
