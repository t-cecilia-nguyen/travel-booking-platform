using GBC_Travel_Group_90.Data;
using Microsoft.AspNetCore.Mvc;
using GBC_Travel_Group_90.Models;
using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.Filters;


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
        public async Task<IActionResult> Index(string email)
        {


            if (email == null || string.IsNullOrEmpty(email))
            {
                ViewBag.IsAdmin = false;
                return View(await _db.CarRentals.ToListAsync());
            }

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsAdmin);
            if (user != null)
            {
                Console.WriteLine("admin is true");
                ViewBag.IsAdmin = true;
            }

            return View(await _db.CarRentals.ToListAsync());
        }


        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var carRental = await _db.CarRentals.FirstOrDefaultAsync(p => p.CarRentalId == id);

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
        public async Task<IActionResult> Success(int carRentalId, int userId)
        {
            var user = await _db.Users.FindAsync(userId);
            var carRental = await _db.CarRentals.FindAsync(carRentalId);

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
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> Create(CarRental carRental)
        {
            if (ModelState.IsValid)
            {
                await _db.CarRentals.AddAsync(carRental);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(carRental);
        }



        [HttpGet("Book/{id:int}")]
        public async Task<IActionResult> Book(int id)
        {
            var carRental = await _db.CarRentals.FindAsync(id);

            if (carRental == null)
            {
                return NotFound();
            }

            return View(carRental);
        }



        [ServiceFilter(typeof(LoggingFilter))]
        [HttpPost("Book/{id:int}")]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> Book(int id, string userEmail)
        {

            var carRental = await _db.CarRentals.FindAsync(id);

            if (carRental == null)
            {
                return NotFound(); // Car rental not found, return Not Found status
            }

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                user = new User
                {
                    Email = userEmail,
                    FirstName = "Guest",
                    LastName = "Guest"
                };
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
            }

            ViewBag.User = user;

            ViewBag.CarRental = carRental;

            carRental.Available = false;
            carRental.UserId = user.UserId;
            await _db.SaveChangesAsync();


            return RedirectToAction("Success", new { carRentalId = carRental.CarRentalId, userId = user.UserId });
        }


        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var carRental = await _db.CarRentals.FindAsync(id);

            if (carRental == null)
            {
                return NotFound();
            }
            return View(carRental);
        }



        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> Edit(int id, [Bind("CarRentalId, RentalCompany, PickUpLocation, PickUpDate, DropOffDate, CarModel, Price")] CarRental carRental)
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
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarRentalExists(carRental.CarRentalId))
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



        private async Task<bool> CarRentalExists(int id)
        {
            return await _db.CarRentals.AnyAsync(e => e.CarRentalId == id);
        }


        [HttpGet("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var carRental = await _db.CarRentals.FirstOrDefaultAsync(p => p.CarRentalId == id);

            if (carRental == null)
            {
                return NotFound();
            }
            return View(carRental);
        }



        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int carRentalId)
        {
            var carRental = await _db.CarRentals.FindAsync(carRentalId);
            if (carRental != null)
            {
                _db.CarRentals.Remove(carRental);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }


        [ServiceFilter(typeof(LoggingFilter))]
        [HttpGet("Search")]
        [Route("Search/{serachType?}/{RentalCompany?}/{CarModel?}/{PickUpDate?}/{DropoffDate?}")]
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