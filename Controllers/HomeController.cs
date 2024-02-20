using GBC_Travel_Group_90.Data;
using GBC_Travel_Group_90.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GBC_Travel_Group_90.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
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
                    IsAdmin = false
                };

                _db.Users.Add(newUser);
                _db.SaveChanges();

                return RedirectToAction("Index", "Flight");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
