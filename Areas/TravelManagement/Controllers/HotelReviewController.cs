using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Controllers
{
    [Area("TravelManagement")]
    [Route("[area]/[controller]/[action]")]
    public class HotelReviewController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HotelReviewController(ApplicationDbContext context)
        {
            _db = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetHotelReviews(int hotelId)
        {
            var hotelReviews = await _db.HotelReviews
                                    .Where(c => c.HotelId == hotelId)
                                    .OrderByDescending(c => c.CreatedDate)
                                    .ToListAsync();

            return Json(hotelReviews);
        }


        [HttpPost]
        public async Task<IActionResult> AddHotelReview([FromBody] HotelReview hotelReview)
        {
            if (ModelState.IsValid)
            {
                hotelReview.CreatedDate = DateTime.Now; // Set the current time as the posting time
                _db.HotelReviews.Add(hotelReview);
                await _db.SaveChangesAsync();
                return Json(new { success = true, message = "Review added successfully." });
            }

            // Log ModelState errors
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, message = "Invalid review data.", errors = errors });
        }
    }
}
