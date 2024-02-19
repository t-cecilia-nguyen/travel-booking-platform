using GBC_Travel_Group_90.Data;
using Microsoft.AspNetCore.Mvc;
using GBC_Travel_Group_90.Models;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_90.Controllers
{
	public class CarRentalController : Controller
	{
		private readonly ApplicationDbContext _db;

		public CarRentalController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			return View(_db.CarRentals.ToList());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(CarRental carRental)
		{
			if (ModelState.IsValid)
			{
				_db.CarRentals.Add(carRental);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(carRental);
		}
		public IActionResult Details(int id)
		{
			var carRental = _db.CarRentals.FirstOrDefault(p => p.CarRentalId == id);

			if (carRental == null)
			{
				return NotFound();
			}
			return View(carRental);
		}
		public IActionResult Edit(int id)
		{
			var carRental = _db.CarRentals.Find(id);

			if (carRental == null)
			{
				return NotFound();
			}
			return View(carRental);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("CarRentalId, RentalCompany, PickUpLocation, PickUpDate, DropOffDate, CarModel, Price")] CarRental carRental)
		{
			if (id != carRental.CarRentalId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_db.Update(carRental);
					_db.SaveChanges();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CarRentalExists(carRental.CarRentalId))
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
			return View(carRental);
		}

		private bool CarRentalExists(int id)
		{
			return _db.CarRentals.Any(e => e.CarRentalId == id);
		}

		public IActionResult Delete(int id)
		{
			var carRental = _db.CarRentals.FirstOrDefault(p => p.CarRentalId == id);

			if (carRental == null)
			{
				return NotFound();
			}
			return View(carRental);
		}

		[HttpPost, ActionName("DeleteConfirmed")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int carRentalId)
		{
			var carRental = _db.CarRentals.Find(carRentalId);
			if (carRental != null)
			{
				_db.CarRentals.Remove(carRental);
				_db.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			return NotFound();
		}
	}
}
