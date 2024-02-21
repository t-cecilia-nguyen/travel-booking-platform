using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_90.Data;
using GBC_Travel_Group_90.Models;

namespace GBC_Travel_Group_90.Controllers
{

    [Route("User")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }*/

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("Details")]
        public IActionResult Details(string email)
        {
            var user = _context.Users.FirstOrDefault(p => p.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            var userFlights = _context.Bookings
                            .Where(b => b.UserId == user.UserId)
                            .Include(b => b.Flight)
                            .ToList();

            var userCarRentals = _context.CarRentals.Where(cr => cr.UserId == user.UserId).ToList();

            // Create a ViewModel to hold all this data
            var viewModel = new ViewModel
            {
                User = user,
                Bookings = userFlights,
                CarRentals = userCarRentals

            };
            ViewBag.User = user;
            ViewBag.Email = user.Email;
            return View(viewModel);

            /*// Pass both the user and the list of car rentals to the view
            ViewBag.User = user;
            return View(userCarRentals);*/
        }


        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,Email,IsAdmin")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();

                ViewBag.UserId = user.UserId; 
                ViewBag.UserEmail = user.Email;
                ViewBag.IsAdmin = user.IsAdmin;


                return RedirectToAction(nameof(Index), "Hotel", new {userId = user.UserId});
            }
            return View(user);
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,FirstName,LastName,Email,IsAdmin")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost("Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
