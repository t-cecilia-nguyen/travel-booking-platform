using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_90.Data;
using GBC_Travel_Group_90.Areas.TravelManagement.Models;

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
        public async Task<IActionResult> Index(string email, bool isAdmin = false)
        {

            ViewBag.Email = email;


            var applicationDbContext = _context.Hotels;

            //handle isAdmin request
            ViewBag.IsAdmin = isAdmin;
            if (isAdmin)
            {
                ViewBag.IsAdmin = true;
                return View(await applicationDbContext.ToListAsync());
            }


            if (email == null || string.IsNullOrEmpty(email))
            {
                ViewBag.IsAdmin = false;
                return View(await applicationDbContext.ToListAsync());
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsAdmin);
            if (user != null)
            {
                Console.WriteLine("admin is true");

                ViewBag.IsAdmin = true;
            }

            return View(await applicationDbContext.ToListAsync());


        }

        // GET: Hotels/Details/5
        public async Task<IActionResult> Details(int? id, bool isAdmin = false)
        {
            ViewBag.IsAdmin = false;
            if (id == null)
            {
                return NotFound();
            }

            if (isAdmin)
            {
                ViewBag.IsAdmin = true;
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
                await _context.AddAsync(hotel);
                await _context.SaveChangesAsync();
                //return isAdmin's View
                return RedirectToAction(nameof(Index), new { isAdmin = true });
            }

            return View(hotel);
        }

        /*// GET: Hotels/Edit/5
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
        }*/


        [HttpGet("Edit/{id:int}")]
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
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
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

        [HttpGet("SearchHotel")]
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

            return View("Index", hotels);
        }

    }
}
