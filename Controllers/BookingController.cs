using GBC_Travel_Group_90.Data;
using GBC_Travel_Group_90.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_90.Controllers
{
	public class BookingController : Controller
	{
		private readonly ApplicationDbContext _db;

		public BookingController(ApplicationDbContext db)
		{
			_db = db;
		}
		public async Task<IActionResult> Index(int userId)
		{
			var bookings = await _db.Bookings
				.Include(b => b.User)
				.Include(b => b.Flight)
				.Include(b => b.Hotel)
				.Include(b => b.CarRental)
				.Where(b => b.UserId == userId)
				.ToListAsync();

			return View(bookings);
		}

		[HttpPost]
		public IActionResult BookFlight(string email, int flightId)
		{
			// Check if the email exists
			var user = _db.Users.FirstOrDefault(u => u.Email == email);
			if (user != null)
			{
				var flight = _db.Flights.FirstOrDefault(f => f.FlightId == flightId);
				var booking = new Booking
				{
					User = user,
					UserId = user.UserId,
					FlightId = flightId,
					Flight = flight
				};
				_db.Bookings.Add(booking);
				flight.CurrentPassengers++;
				_db.SaveChanges();
				return RedirectToAction("Index", new { userId = user.UserId });
			}
			else
			{
				/*ModelState.AddModelError("email", "Email address not registered.");*/
				return View("Index", "Flight"); // Need Error Page
			}
		}
	}

}
