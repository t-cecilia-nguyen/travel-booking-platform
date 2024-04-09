using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Controllers
{
    [Area("TravelManagement")]
    [Route("[area]/[controller]/[action]")]
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            user.Bookings = await _db.Bookings
                                    .Where(b => b.ApplicationUserId == user.Id)
                                    .Include(b => b.Flight)
                                    .ToListAsync();

            user.CarSuccesses = await _db.CarSuccess
                                    .Where(cs => cs.ApplicationUserId == user.Id)
                                    .Include(cs => cs.CarRental)
                                    .ToListAsync();

            user.HotelBookings = await _db.HotelBookings
                                    .Where(hb => hb.ApplicationUserId == user.Id)
                                    .Include(hb => hb.Hotel)
                                    .ToListAsync();

            return View(user);
        }
    }
}
