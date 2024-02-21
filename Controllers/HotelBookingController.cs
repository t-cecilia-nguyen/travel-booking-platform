using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_90.Data;
using GBC_Travel_Group_90.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HotelBookings.Include(h => h.Hotel).Include(h => h.User);
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
        public IActionResult Create(int hotelId, int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null) return NotFound("User not found");

            var hotel = _context.Hotels.Find(hotelId);
            if (hotel == null) return NotFound("Hotel not found");

            var hotelBooking = new HotelBooking { HotelId = hotelId, UserId = userId , Status = Status.Pending };

            ViewBag.HotelId = hotelId;
            ViewBag.UserId = userId;

            return View(hotelBooking);
        }

        // POST: HotelBookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HotelBookingId, CheckInDate,CheckOutDate, NumOfRoomsToBook,UserId,HotelId")] HotelBooking hotelBooking)
        {
            if (ModelState.IsValid)
            {
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
            //Repopulate the hotel selectList if returning to the form

            return View(hotelBooking);
        }

        // POST: HotelBookings/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HotelBookingId,CheckInDate,CheckOutDate,UserId,HotelId")] HotelBooking hotelBooking)
        {
            if (id != hotelBooking.HotelBookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
