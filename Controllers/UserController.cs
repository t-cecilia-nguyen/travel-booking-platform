using GBC_Travel_Group_90.Data;
using GBC_Travel_Group_90.Models;
using Microsoft.AspNetCore.Mvc;

namespace GBC_Travel_Group_90.Controllers
{

	[Route("User")]
	public class UserController : Controller
	{

		private readonly ApplicationDbContext _db;

		public UserController(ApplicationDbContext db)
		{
			_db = db;
		}



		[HttpGet("")]
		public IActionResult Index()
		{
			return View();
		}



        [HttpGet("Details")]
        public IActionResult Details(string email)
        {
            var user = _db.Users.FirstOrDefault(p => p.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            var userCarRentals = _db.CarRentals.Where(cr => cr.UserId == user.UserId).ToList();

            ViewBag.UserEmail = email; // Pass user email to the view
            return View(userCarRentals);
        }


        [HttpPost]
		public IActionResult Create(User user)
		{
			// Check if the email already exists
			var existingUser = _db.Users.FirstOrDefault(u => u.Email == user.Email);

			if (existingUser != null)
			{
				return RedirectToAction("Index", "Flight");
			}
			else
			{
				// Email doesn't exist, save a new user
				var newUser = new User
				{
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email,
					Admin = false
				};

				_db.Users.Add(newUser);
				_db.SaveChanges();

				return RedirectToAction("Index", "Flight");
			}
		}
	}
}
