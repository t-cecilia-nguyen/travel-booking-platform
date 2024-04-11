using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Controllers
{
    [Area("TravelManagement")]
    [Route("[area]/[controller]/[action]")]
    public class AllBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AllBookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var allBookings = new AllBookings
            {
                Bookings = await _context.Bookings
                                        .Include(b => b.User)
                                        .Include(b => b.Flight)
                                        .ToListAsync(),
                CarSuccesses = await _context.CarSuccess
                                        .Include(b => b.User)
                                        .Include(b => b.CarRental)
                                        .ToListAsync(),
                HotelBookings = await _context.HotelBookings
                                        .Include(b => b.User)
                                        .Include(b => b.Hotel)
                                        .ToListAsync()
            };

            return View(allBookings);
        }
    }
}
