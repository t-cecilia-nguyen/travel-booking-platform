﻿using GBC_Travel_Group_90.Data;
using Microsoft.AspNetCore.Mvc;
using GBC_Travel_Group_90.Models;
using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.Filters;
using GBC_Travel_Group_90.CustomMiddlewares;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SendGrid.Helpers.Mail;


namespace GBC_Travel_Group_90.Areas.TravelManagement.Controllers
{
    [Area("TravelManagement")]
    [Route("[area]/[controller]")]
    public class CarRentalController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CarRentalController> _logger;
        public CarRentalController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, ILogger<CarRentalController> logger)
        {
            _db = db;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var cars = await _db.CarRentals.ToListAsync();
            return View(cars);
        }




        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var carRental = await _db.CarRentals.FirstOrDefaultAsync(p => p.CarRentalId == id);

            if (carRental == null)
            {
                Response.StatusCode = 404;
                ViewBag.CarId = id;
                return View("CarNotFound");
                
            }
            return View(carRental);
        }

        [HttpGet("Create")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet("Success/{carRentalId:int}")]
        public async Task<IActionResult> Success(int carRentalId)
        {
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _db.Users.FindAsync(userId);
            var carRental = await _db.CarRentals.FindAsync(carRentalId);

            if (carRental == null)
            {
                return NotFound();
            }

            var model = new CarSuccess
            {
                User = user,
                ApplicationUserId = userId,
                CarRentalId = carRentalId,
                CarRental = carRental
            };

            await _db.CarSuccess.AddAsync(model);
            await _db.SaveChangesAsync();
            
            _logger.LogInformation("-------- User {fisrtname} {lastname} successfully booked Car: {carInfo}", user.FirstName, user.LastName, model.ToString());
            return View(model);
           
        }


        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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

        [HttpGet("GetBook/{id:int}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var carRental = await _db.CarRentals.FindAsync(id);

            if (carRental == null)
            {
                return NotFound();
            }

            return View("Book", carRental);
        }

        [HttpPost("Book")]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(LoggingFilter))]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> Book(int id)
        {

            var carRental = await _db.CarRentals.FindAsync(id);

            if (carRental == null)
            {
                System.Console.WriteLine("carRental = NUL!!!!!!!!!!!");
                return NotFound(); // Car rental not found, return Not Found status
            }

            string email;
            ApplicationUser user = null;

            // Check if User is signed in
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.GetUserAsync(User);
            }
            else
            {
                // If User is not signed in
                if (user == null)
                {
                    return View("~/Areas/TravelManagement/Views/Booking/NotSignedInOrRegistered.cshtml");
                }
            }

            // Calculate Car Points
            int points = CalculateFrequentCarPoints(carRental.Price);
            user.FrequentCarPoints += points;

            email = user.Email;
            ViewBag.CarRental = carRental;

            carRental.Available = false;
            carRental.ApplicationUserId = user.Id;
            await _db.SaveChangesAsync();


            return RedirectToAction("Success", new { carRentalId = carRental.CarRentalId, userId = user.UserId });

        }

        


        [HttpGet("Edit/{id:int}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var carRental = await _db.CarRentals.FirstOrDefaultAsync(p => p.CarRentalId == id);

            if (carRental == null)
            {
                return NotFound();
            }
            return View(carRental);
        }

        [HttpPost("DeleteConfirmed/{id:int}")]
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
        [Route("Search/{searchType?}/{RentalCompany?}/{CarModel?}/{PickUpDate?}/{DropoffDate?}")]
        public async Task<IActionResult> Search(string? searchType, string RentalCompany, string CarModel, DateTime? PickUpDate, DateTime? DropOffDate)
        {
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

            var cars = await carQuery.ToListAsync();

            // Return partial view with search results
            return PartialView("_CarRentalSearchResults", cars);
        }

        public int CalculateFrequentCarPoints(decimal price)
        {
            int points = (int)Math.Floor(price / 10);
            return points;
        }
    }
}