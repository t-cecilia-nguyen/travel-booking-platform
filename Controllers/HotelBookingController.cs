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

        // GET: HotelBookings/Create
        public IActionResult Create(int hotelId, int userId)
        {
            var hotel = _context.Hotels.Find(hotelId);
            if (hotel == null) return NotFound();

            var hotelBooking = new HotelBooking { HotelId = hotelId, UserId = userId };


            ViewBag.HotelId = hotelId;
            ViewBag.UserId = userId;


            return View(hotelBooking);

        }



        // POST: HotelBookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HotelBookingId,CheckInDate,CheckOutDate,UserId,HotelId")] HotelBooking hotelBooking)
        {


            if (ModelState.IsValid)
            {


                // User exists, proceed with the insertion
                hotelBooking.BookingDate = DateTime.Now;

                _context.HotelBookings.Add(hotelBooking);
                await _context.SaveChangesAsync();
                ViewBag.HotelId = hotelBooking.HotelId;
                ViewBag.UserId = hotelBooking.UserId;

                return RedirectToAction(nameof(Details), new { id = hotelBooking.HotelBookingId });


            }
            return (View(hotelBooking));

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

        private bool HotelBookingExists(int id)
        {
            return _context.HotelBookings.Any(e => e.HotelBookingId == id);
        }
    }
}
