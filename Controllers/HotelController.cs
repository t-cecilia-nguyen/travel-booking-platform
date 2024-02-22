using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_90.Data;
using GBC_Travel_Group_90.Models;
using Microsoft.AspNetCore.Authorization;

namespace GBC_Travel_Group_90.Controllers
{
    
    public class HotelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hotels
        public async Task<IActionResult> Index(int? userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                bool isAdmin = user.IsAdmin;
                string? email = user.Email;
                ViewBag.IsAdmin = isAdmin;
                ViewBag.Email = email;
                ViewBag.UserId = user.UserId;
            }
            
            
            return View(await _context.Hotels.ToListAsync());
        }

        // GET: Hotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
          
            

            var hotel = await _context.Hotels
                .FirstOrDefaultAsync(m => m.HotelId == id);
            if (hotel == null)
            {
                return NotFound();
            }
          
            ViewBag.HotelId = hotel.HotelId;

            return View(hotel);
        }


        // GET: Hotels/Create
       
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hotels/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Location,StarRate,NumberOfRooms,Price")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Edit/5
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Location,StarRate,NumberOfRooms,Price")] Hotel hotel)
        {
            if (id != hotel.HotelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.HotelId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels
                .FirstOrDefaultAsync(m => m.HotelId == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.HotelId == id);
        }

		[HttpGet("SearchHotel")]
		public async Task<IActionResult> SearchHotel(string? name, string? location, int? starRate,DateTime? checkInDate, DateTime? checkOutDate, decimal? maxPrice)
		{
			var hotelsQuery = _context.Hotels.AsQueryable();

			if (!string.IsNullOrEmpty(name))
			{
                hotelsQuery = hotelsQuery.Where(h => h.Name != null && h.Name.Contains(name));

            }

            if (!string.IsNullOrEmpty(location))
			{
				hotelsQuery = hotelsQuery.Where(h => h.Location != null  && h.Location.Contains(location));
			}

			if (starRate > 0 && starRate <= 5)
			{
				hotelsQuery = hotelsQuery.Where(h => h.StarRate == starRate);
			}

            if (checkInDate.HasValue && checkOutDate.HasValue)
            {
                // Retrieve bookings for the specified date range only
                var bookingsForDateRange = _context.HotelBookings
                    .Where(b => b.CheckInDate < checkOutDate && b.CheckOutDate > checkInDate)
                    .ToList();

                // Filter the hotels based on availability
                var availableHotelIds = hotelsQuery
                    .Where(h => h.IsAvailableForDates(checkInDate, checkOutDate, bookingsForDateRange))
                    .Select(h => h.HotelId) 
                    .ToList();

                // Filter the hotelsQuery based on the availableHotelIds
                hotelsQuery = hotelsQuery.Where(h => availableHotelIds.Contains(h.HotelId));
            }

            if (maxPrice.HasValue)
			{
               
                 hotelsQuery = hotelsQuery.Where(h => h.Price <= maxPrice.Value);
            }

            


            var hotels = await hotelsQuery.ToListAsync();

			return View("Index", hotels); // Reuse the Index view to display results
		}

	}
}
