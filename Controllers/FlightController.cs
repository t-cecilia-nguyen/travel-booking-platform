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
        public IActionResult Index()
        {
            // Test with hard coding
            //var flights = new List<Flight>()
            //{
            //    new Flight { FlightId = 1,
            //        FlightNumber = "F8 1602",
            //        Airline = "Flair Airlines",
            //        Origin = "Toronto",
            //        Destination = "Fort Lauderdale",
            //        DepartureTime = new DateTime(2024,4,1,6,30,0),
            //        ArrivalTime = new DateTime(2024,4,1,9,50,0),
            //        Price = 131
            //    }

            //};

			return View(_db.Flights.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Flight flight)
        {
            if(ModelState.IsValid)
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
        public IActionResult Edit(int id)
        {
            var flight = _db.Flights.Find(id);

            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FlightId, FlightNumber, Airline, Origin, Destination, DepartureTime, ArrivalTime, Price")] Flight flight)
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
    }
}
