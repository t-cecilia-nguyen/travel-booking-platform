
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_90.Data;
using GBC_Travel_Group_90.Models;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace GBC_Travel_Group_90.Controllers
{
    public class HotelBookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelBookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HotelBookings
        public async Task<IActionResult> Index(string email)
        {
            ViewBag.IsAdmin = false;
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
        public async Task<IActionResult> Details(int? id)
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


        private bool IsRoomAvailable(int? hotelId, int numOfRoomsToBook)
        {
            var hotel = _context.Hotels.Find(hotelId);

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

        // GET: HotelBookings/Create
        public IActionResult Create(int hotelId)
        {

            var hotel = _context.Hotels.Find(hotelId);
            if (hotel == null) return NotFound("Hotel not found");

           
            var hotelBooking = new HotelBooking { HotelId = hotelId, Status = Status.Pending };

            ViewBag.HotelId = hotelId;
            

            return View(hotelBooking);
        }

        // POST: HotelBookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HotelBookingId, CheckInDate,CheckOutDate, NumOfRoomsToBook,HotelId")] HotelBooking hotelBooking, string userEmail)
        {
            if (ModelState.IsValid)
            {

                // Check if the user already booked the hotel
                var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
                if (user == null)
                {
                    user = new User
                    {
                        Email = userEmail,
                        FirstName = "Guest",
                        LastName = "Guest"
                    };
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }

                var existingBooking = _context.HotelBookings.FirstOrDefault(b => b.UserId == user.UserId && b.HotelId == hotelBooking.HotelId);

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
                if (IsRoomAvailable(hotelBooking.HotelId, hotelBooking.NumOfRoomsToBook))
                {
                    // Update the NumberOfRooms property
                    UpdateNumberOfRooms(hotelBooking.HotelId, hotelBooking.NumOfRoomsToBook);

                   
                    // Continue booking 
                    
                    hotelBooking.UserId = user.UserId;
                    hotelBooking.BookingDate = DateTime.Now;
                    hotelBooking.Status = Status.Confirmed; 
                    _context.HotelBookings.Add(hotelBooking);
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
        public async Task<IActionResult> Edit(int? id)
        {
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
        public async Task<IActionResult> Edit(int id, [Bind("HotelBookingId, NumOfRoomsToBook, CheckInDate,CheckOutDate,UserId, HotelId")] HotelBooking hotelBooking)
        {
            if (id != hotelBooking.HotelBookingId)
            {
                return NotFound();
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
                    if (!HotelBookingExists(hotelBooking.HotelBookingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new {id = hotelBooking.HotelBookingId});
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

        private bool HotelBookingExists(int id)
        {
            return _context.HotelBookings.Any(e => e.HotelBookingId == id);
        }
    }
}
