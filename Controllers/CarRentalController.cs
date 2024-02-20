using GBC_Travel_Group_90.Data;
using Microsoft.AspNetCore.Mvc;
using GBC_Travel_Group_90.Models;
using Microsoft.EntityFrameworkCore;


namespace GBC_Travel_Group_90.Controllers
{

    [Route("Car")]
    public class CarRentalController : Controller
	{
		private readonly ApplicationDbContext _db;


        public CarRentalController(ApplicationDbContext db)
        {
            _db = db;
        }



        [HttpGet("")]
        public IActionResult Index()
		{
			return View(_db.CarRentals.ToList());
		}



        [HttpGet("Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var carRental = _db.CarRentals.FirstOrDefault(p => p.CarRentalId == id);

            if (carRental == null)
            {
                return NotFound();
            }
            return View(carRental);
        }



        [HttpGet("Create")]
        public IActionResult Create()
        {

            return View();
        }



        [HttpGet("Success")]
        public IActionResult Success()
        {
            return View();
        }



        [HttpPost("Create")]
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



		[HttpGet("Book/{id:int}")]
		public IActionResult Book(int id)
		{
			var carRental = _db.CarRentals.Find(id);

			if (carRental == null)
			{
				return NotFound();
			}

			return View(carRental);
		}




        [HttpPost("Book/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Book(int id, string userEmail)
        {
            var carRental = _db.CarRentals.Find(id); // Find the car rental by id

            if (carRental == null)
            {
                return NotFound(); // Car rental not found, return Not Found status
            }


            var user = _db.Users.FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
            {
                user = new User
                {
                    Email = userEmail,
                    FirstName = "Guest",
                    LastName = "Guest"
                };
                _db.Users.Add(user); 
                _db.SaveChanges(); 
            }

            carRental.Available = false;
            carRental.UserId = user.UserId; 
            _db.SaveChanges();


            return RedirectToAction("Success", "Car");
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



        [HttpGet("Edit/{id:int}")]
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
