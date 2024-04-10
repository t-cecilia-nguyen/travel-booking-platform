using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Controllers
{
    [Area("TravelManagement")]
    [Route("[area]/[controller]/[action]")]
    public class CarRentalReviewController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        public CarRentalReviewController(ApplicationDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarRentalReviews(int carRentalId)
        {
            var carRentalReviews = await _db.CarRentalReviews
                                    .Where(c => c.CarRentalId == carRentalId)
                                    .OrderByDescending(c => c.CreatedDate)
                                    .ToListAsync();

            return Json(carRentalReviews);
        }

        [HttpPost]
        public async Task<IActionResult> AddCarRentalReview([FromBody] CarRentalReview carRentalReview)
        {
            if (ModelState.IsValid)
            {
                carRentalReview.CreatedDate = DateTime.Now; // Set the current time as the posting time
                _db.CarRentalReviews.Add(carRentalReview);
                await _db.SaveChangesAsync();
                return Json(new { success = true, message = "Review added successfully." });
            }

            // Log ModelState errors
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, message = "Invalid review data.", errors = errors });
        }      
    }
}
