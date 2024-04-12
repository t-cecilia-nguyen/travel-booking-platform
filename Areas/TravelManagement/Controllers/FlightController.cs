using Microsoft.AspNetCore.Mvc;
using GBC_Travel_Group_90.Data;
using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using Microsoft.AspNetCore.Authorization;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Controllers
{
    [Area("TravelManagement")]
    [Route("[area]/[controller]/[action]")]
    public class FlightController : Controller
    {
        private readonly ApplicationDbContext _db;

        public FlightController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var flights = await _db.Flights.ToListAsync();
            var availableFlights = flights.Where(f => f.CurrentPassengers < f.MaxPassengers).ToList();
            return View(availableFlights);
        }

        
        [HttpGet("Create")]
        [Authorize(Roles = "Admin")]
		public IActionResult Create()
        {
            return View();
        }

		
		[HttpPost("Create")]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create(Flight flight)
        {
            if (ModelState.IsValid)
            {
                await _db.Flights.AddAsync(flight);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var flight = await _db.Flights.FirstOrDefaultAsync(p => p.FlightId == id);

            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

		[HttpGet("Edit/{id:int}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int id)
        {
            var flight = await _db.Flights.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }
		
		[HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int id, [Bind("FlightId, FlightNumber, Airline, Origin, Destination, DepartureTime, ArrivalTime, Price, MaxPassengers")] Flight flight)
        {
            if (id != flight.FlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(flight);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await FlightExists(flight.FlightId))
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
            return View(flight);
        }

        private async Task<bool> FlightExists(int id)
        {
            return await _db.Flights.AnyAsync(e => e.FlightId == id);
        }

		[HttpGet("Delete/{id:int}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int id)
        {
            var flight = await _db.Flights.FirstOrDefaultAsync(p => p.FlightId == id);

            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

		[HttpPost("DeleteConfirmed/{id:int}")]
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmed(int flightId)
        {
            var flight = await _db.Flights.FindAsync(flightId);
            if (flight != null)
            {
                _db.Flights.Remove(flight);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

		[HttpPost("Search")]
		public async Task<IActionResult> Search(string origin, string destination, DateTime? departureDate, DateTime? arrivalDate)
		{
			ViewBag.IsAdmin = false;

			var flightsQuery = from f in _db.Flights select f;

			if (!string.IsNullOrEmpty(origin))
			{
				flightsQuery = flightsQuery.Where(f => f.Origin.Contains(origin));
			}

			if (!string.IsNullOrEmpty(destination))
			{
				flightsQuery = flightsQuery.Where(f => f.Destination.Contains(destination));
			}

			if (departureDate.HasValue)
			{
				flightsQuery = flightsQuery.Where(f => f.DepartureTime.Date == departureDate.Value.Date);
			}

			if (arrivalDate.HasValue)
			{
				flightsQuery = flightsQuery.Where(f => f.ArrivalTime.Date == arrivalDate.Value.Date);
			}

			var flights = await flightsQuery.ToListAsync();

			return PartialView("_FlightSearchResult", flights); // Return partial view
		}
	}
}
