using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_90.Data;
using GBC_Travel_Group_90.Models;
using Microsoft.CodeAnalysis;
using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.Filters;
using System.Linq.Expressions;
using GBC_Travel_Group_90.CustomMiddlewares.GBC_Travel_Group_90.CustomMiddlewares;
using Microsoft.AspNetCore.Identity;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Controllers
{
    [Area("TravelManagement")]
    [Route("[area]/[controller]")]
    public class HotelBookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HotelBookingController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: HotelBookings
        public async Task<IActionResult> Index(string email)
        {

            var applicationDbContext = _context.HotelBookings.Include(h => h.Hotel).Include(h => h.User);

            if (email == null || string.IsNullOrEmpty(email))
            {
                return View(await applicationDbContext.ToListAsync());
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return View(await applicationDbContext.ToListAsync());
        }

        
        // GET: HotelBooking/Details/5
        [HttpGet("Details/{name}/{id:int}")]
        public async Task<IActionResult> Details(string name, int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var hotelBooking = await _context.HotelBookings
                .Include(h => h.Hotel)
                .Include(h => h.User)
                .FirstOrDefaultAsync(m => m.HotelBookingId == id && m.Hotel.Name == name);
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
                _context.Hotels.Update(hotel);
                _context.SaveChanges();
            }
        }
        private bool IsValidBookingDates(DateTime checkInDate, DateTime checkOutDate)
        {
            // Ensure that CheckInDate is smaller than or equal to CheckOutDate
            return checkInDate <= checkOutDate;
        }



        [ServiceFilter(typeof(LoggingFilter))]
        [HttpGet, ActionName("CreateBooking")]
        [Route("CreateBooking/{name?}/{hotelId:int?}")]
        public async Task<IActionResult> CreateBooking(string? name, int hotelId)
        {

            var hotel = await _context.Hotels.FindAsync(hotelId);
            if (hotel == null)
            {
                Response.StatusCode = 400;
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = (int)Response.StatusCode });
            }

            var hotelBooking = new HotelBooking { Hotel = hotel, HotelId = hotelId, Status = Status.Pending };
            
            ViewBag.HotelId = hotelId;
            ViewBag.Name = name;

            return View(hotelBooking);
        }


        [HttpPost("CreateBooking/{name}/{hotelId:int}")]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(LoggingFilter))]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> CreateBooking([Bind("HotelBookingId, CheckInDate,CheckOutDate, NumOfRoomsToBook,HotelId, Name")] HotelBooking hotelBooking)
        {
            string email;
            ApplicationUser user = null;

            // If User is signed in
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.GetUserAsync(User);                

            } else
            {
                // If User is not signed in
                if (user == null)
                {
                    return View("NotSignedInOrRegistered");
                }
            }

            email = user.Email;

            if (ModelState.IsValid)
            {
                // Check if the user already booked the hotel
                var existingBooking = await _context.HotelBookings.FirstOrDefaultAsync(b => b.ApplicationUserId == user.Id && b.HotelId == hotelBooking.HotelId);

                if (existingBooking != null)
                {
                    TempData["AlreadyBooked"] = "You have already booked this hotel.";
                    return RedirectToAction("Index", "Hotel", new { email = user.Email });
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

                    // Calculate Hotel Loyalty Points                    
                    var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.HotelId == hotelBooking.HotelId);
                    decimal hotelPrice = hotel.Price;

                    int numberOfDays = (hotelBooking.CheckOutDate - hotelBooking.CheckInDate).Days;
                    decimal totalPrice = hotelPrice * numberOfDays * hotelBooking.NumOfRoomsToBook;

                    int points = CalculateHotelLoyaltyPoints(totalPrice);
                    user.HotelLoyaltyPoints += points;

                    // Continue booking 
                    hotelBooking.TotalAmount = totalPrice;
                    hotelBooking.ApplicationUserId = user.Id;
                    hotelBooking.BookingDate = DateTime.Now;
                    hotelBooking.Status = Status.Confirmed;
                    await _context.HotelBookings.AddAsync(hotelBooking);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Details), new { name = hotelBooking.Name, id = hotelBooking.HotelBookingId });
                }
                else
                {
                    // Display error message to the user
                    ModelState.AddModelError(nameof(hotelBooking.NumOfRoomsToBook), "Not enough rooms available for booking.");

                    ViewBag.HotelId = hotelBooking.HotelId;
                    ViewBag.UserId = hotelBooking.ApplicationUserId;

                    return View(hotelBooking);
                }

                
            }
            return View(hotelBooking);


        }

        // GET: HotelBookings/Edit/5
        [HttpGet("Edit/{name}/{id:int}")]
        public async Task<IActionResult> Edit(string? name, int? id)
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

        [HttpPost("Edit/{id:int}")]
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
        public int CalculateHotelLoyaltyPoints(decimal price)
        {
            int points = (int)Math.Floor(price / 100);
            return points;
        }
    }
}
