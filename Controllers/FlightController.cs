using GBC_Travel_Group_90.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;
using System;
using System.Text;
using GBC_Travel_Group_90.Data;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_90.Controllers
{
    public class FlightController : Controller
    {
        private readonly ApplicationDbContext _db;

        public FlightController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string email)
        {
			ViewBag.IsAdmin = false;

            // If not Admin - view available Flights
			if (email == null || string.IsNullOrEmpty(email))
			{
				ViewBag.IsAdmin = false;
				var flights = _db.Flights.ToList();
				var availableFlights = flights.Where(f => f.CurrentPassengers < f.MaxPassengers).ToList();
				return View(availableFlights);
			}

            // If Admin - view all Flights
			var user = _db.Users.FirstOrDefault(u => u.Email == email && u.IsAdmin);
			if (user != null)
			{
				Console.WriteLine("admin is true");
				ViewBag.IsAdmin = true;
			}

			var allflights = _db.Flights.ToList();
            return View(allflights);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Flight flight)
        {
            if (ModelState.IsValid)
            {
                _db.Flights.Add(flight);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flight);
        }
        public IActionResult Details(int id)
        {
            var flight = _db.Flights.FirstOrDefault(p => p.FlightId == id);

            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        [HttpGet("Edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var flight = _db.Flights.Find(id);

            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FlightId, FlightNumber, Airline, Origin, Destination, DepartureTime, ArrivalTime, Price, MaxPassengers")] Flight flight)
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
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.FlightId))
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

        private bool FlightExists(int id)
        {
            return _db.Flights.Any(e => e.FlightId == id);
        }

        public IActionResult Delete(int id)
        {
            var flight = _db.Flights.FirstOrDefault(p => p.FlightId == id);

            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int flightId)
        {
            var flight = _db.Flights.Find(flightId);
            if (flight != null)
            {
                _db.Flights.Remove(flight);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        [HttpGet("Search")]
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

            return View("Index", flights); // Reuse the Index view to display results
        }
    }
}
