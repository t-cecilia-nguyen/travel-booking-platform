using GBC_Travel_Group_90.Data;
using GBC_Travel_Group_90.Models;
using Microsoft.AspNetCore.Mvc;

namespace GBC_Travel_Group_90.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookingController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Create(int flightId, User user)
        {
            var booking = new Booking { User = user, FlightId = flightId };            
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightId,OtherProperties")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _db.Add(booking);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }
    }

}
