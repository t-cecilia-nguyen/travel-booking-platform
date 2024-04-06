using GBC_Travel_Group_90.Data;
using Microsoft.AspNetCore.Mvc;
using GBC_Travel_Group_90.Models;
using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_90.Areas.TravelManagement.Models;


namespace GBC_Travel_Group_90.Areas.TravelManagement.Controllers
{

    [Area("TravelManagement")]
    [Route("[area]/[controller]/[action]")]
    public class CarRentalController : Controller
    {
        private readonly ApplicationDbContext _db;


        public CarRentalController(ApplicationDbContext db)
        {
            _db = db;
        }



        [HttpGet("")]
        public IActionResult Index(string email)
        {


            if (email == null || string.IsNullOrEmpty(email))
            {
                ViewBag.IsAdmin = false;
                return View(_db.CarRentals.ToList());
            }

            var user = _db.Users.FirstOrDefault(u => u.Email == email && u.IsAdmin);
            if (user != null)
            {
                Console.WriteLine("admin is true");
                ViewBag.IsAdmin = true;
            }

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


        [HttpGet("Success/{carRentalId:int}/{userId:int}")]
        public IActionResult Success(int carRentalId, int userId)
        {
            var user = _db.Users.Find(userId);
            var carRental = _db.CarRentals.Find(carRentalId);

            if (carRental == null)
            {
                return NotFound();
            }

            var model = new CarSuccess
            {
                User = user,
                CarRental = carRental
            };

            return View(model);
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

            var carRental = _db.CarRentals.Find(id);

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

            ViewBag.User = user;

            ViewBag.CarRental = carRental;

            carRental.Available = false;
            carRental.UserId = user.UserId;
            _db.SaveChanges();


            return RedirectToAction("Success", new { carRentalId = carRental.CarRentalId, userId = user.UserId });
        }


        [HttpGet("Edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var carRental = _db.CarRentals.Find(id);

            if (carRental == null)
            {
                return NotFound();
            }
            return View(carRental);
        }



        [HttpPost("Edit/{id:int}")]
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


        [HttpGet("Delete/{id:int}")]
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



        [HttpGet("Search")]
        public async Task<IActionResult> Search(string RentalCompany, string CarModel, DateTime? PickUpDate, DateTime? DropOffDate)
        {
            ViewBag.IsAdmin = false;
            var carQuery = from f in _db.CarRentals select f;

            if (!string.IsNullOrEmpty(RentalCompany))
            {
                carQuery = carQuery.Where(f => f.RentalCompany.Contains(RentalCompany));
            }

            if (!string.IsNullOrEmpty(CarModel))
            {
                carQuery = carQuery.Where(f => f.CarModel.Contains(CarModel));
            }

            if (PickUpDate.HasValue)
            {
                carQuery = carQuery.Where(f => f.PickUpDate.Date == PickUpDate.Value.Date);
            }

            if (DropOffDate.HasValue)
            {
                carQuery = carQuery.Where(f => f.DropOffDate.Date == DropOffDate.Value.Date);
            }

            var car = await carQuery.ToListAsync();

            return View("Index", car); // Reuse the Index view to display results
        }




    }
}