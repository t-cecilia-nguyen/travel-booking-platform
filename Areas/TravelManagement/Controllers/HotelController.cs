using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_90.Data;
using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using Microsoft.AspNetCore.Authorization;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Controllers
{
    [Area("TravelManagement")]
    [Route("[area]/[controller]/[action]")]
    public class HotelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hotels
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {

            var applicationDbContext = _context.Hotels;
            return View(await applicationDbContext.ToListAsync());
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
        [HttpGet("Create")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hotels/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Name,Location,StarRate,NumberOfRooms,Price")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(hotel);
        }

        [HttpGet("Edit/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Edit/5

        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("HotelId,Name,Location,StarRate,NumberOfRooms,Price")] Hotel hotel)
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
                    if (!await HotelExists(hotel.HotelId))
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
        [HttpGet("Delete")]
        [Authorize(Roles = "Admin")]
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
        [HttpPost("DeleteConfirmed/{id:int}")]
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _context.Hotels.AnyAsync(e => e.HotelId == id);
        }

        [HttpPost("SearchHotel")]
        public async Task<IActionResult> SearchHotel(string? name, string? location, int? starRate, DateTime? checkInDate, DateTime? checkOutDate, decimal? maxPrice)
        {

            var hotelsQuery = _context.Hotels.AsQueryable();


            ViewBag.IsAdmin = false;

            if (!string.IsNullOrEmpty(name))
            {
                hotelsQuery = hotelsQuery.Where(h => h.Name != null && h.Name.Contains(name));

            }

            if (!string.IsNullOrEmpty(location))
            {
                hotelsQuery = hotelsQuery.Where(h => h.Location != null && h.Location.Contains(location));
            }

            if (starRate > 0 && starRate <= 5)
            {
                hotelsQuery = hotelsQuery.Where(h => h.StarRate == starRate);
            }

            if (checkInDate.HasValue && checkOutDate.HasValue)
            {
                // Retrieve bookings for the specified date range only
                var bookingsForDateRange = await _context.HotelBookings
                    .Where(b => b.CheckInDate < checkOutDate && b.CheckOutDate > checkInDate)
                    .ToListAsync();

                // Filter the hotels based on availability
                var availableHotelIds = await hotelsQuery
                    .Where(h => h.IsAvailableForDates(checkInDate, checkOutDate, bookingsForDateRange))
                    .Select(h => h.HotelId)
                    .ToListAsync();

                // Filter the hotelsQuery based on the availableHotelIds
                hotelsQuery = hotelsQuery.Where(h => availableHotelIds.Contains(h.HotelId));
            }

            if (maxPrice.HasValue)
            {

                hotelsQuery = hotelsQuery.Where(h => h.Price <= maxPrice.Value);
            }

            var hotels = await hotelsQuery.ToListAsync();

            return PartialView("_HotelSearchResults", hotels); // Return partial view

        }

    }
}
