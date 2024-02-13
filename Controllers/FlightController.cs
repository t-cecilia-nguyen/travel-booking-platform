using GBC_Travel_Group_90.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;
using System;
using System.Text;

namespace GBC_Travel_Group_90.Controllers
{
    public class FlightController : Controller
    {
        public IActionResult Index()
        {
            var flights = new List<Flight>()
            {
                new Flight { FlightId = 1,
                    FlightNumber = "F8 1602",
                    Airline = "Flair Airlines",
                    Origin = "Toronto",
                    Destination = "Fort Lauderdale",
                    DepartureTime = new DateTime(2024,4,1,6,30,0),
                    ArrivalTime = new DateTime(2024,4,1,9,50,0),
                    Price = 131
                }

            };

			return View(flights);
        }

        public IActionResult Create(Flight flight)
        {
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var flight = new Flight
            {
                FlightId = 1,
                FlightNumber = "F8 1602",
                Airline = "Flair Airlines",
                Origin = "Toronto",
                Destination = "Fort Lauderdale",
                DepartureTime = new DateTime(2024, 4, 1, 6, 30, 0),
                ArrivalTime = new DateTime(2024, 4, 1, 9, 50, 0),
                Price = 131
            };

			return View(flight);
        }
    }
}
