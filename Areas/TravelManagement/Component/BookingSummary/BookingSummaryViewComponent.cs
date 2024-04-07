using GBC_Travel_Group_90.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Component.BookingSummary
{
    public class BookingSummaryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public BookingSummaryViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int bookingId)
        {
            var booking = await _context.Bookings.Include(p => p.Flight).FirstOrDefaultAsync(p => p.BookingId == bookingId);

            if (booking == null)
            {
                return Content("Booking not found.");
            }
            return View("~/Views/Shared/Component/BookingSummary/Default.cshtml", booking);
        }
    }
}
