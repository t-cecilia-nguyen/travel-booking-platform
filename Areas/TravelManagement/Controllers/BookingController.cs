﻿using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Controllers
{
    [Area("TravelManagement")]
    [Route("[area]/[controller]/[action]")]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet("GetBookFlight/{id:int}")]
        public async Task<IActionResult> GetBookFlight(int id)
        {
            var flight = await _db.Flights.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            return View("BookFlight", flight);
        }

        [HttpPost("PostBookFlight")]
        public async Task<IActionResult> PostBookFlight(int id)
        {

            var flight = await _db.Flights.FirstOrDefaultAsync(f => f.FlightId == id);

            if (flight == null)
            {
                return NotFound();
            }

            string email;
            ApplicationUser user = null;

            // If User is signed in
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.GetUserAsync(User);
            }
            else
            {
                // If User is not signed in
                if (user == null)
                {
                    return View("NotSignedInOrRegistered");
                }
               
            }

            email = user.Email;
            // Check if the user already booked flight
            var existingBooking = await _db.Bookings.FirstOrDefaultAsync(b => b.ApplicationUserId == user.Id && b.FlightId == id);

            if (existingBooking != null)
            {
                return View("AlreadyBooked");
            }

            var booking = new Booking
            {
                User = user,
                ApplicationUserId = user.Id,
                FlightId = id,
                Flight = flight
            };

            await _db.Bookings.AddAsync(booking);
            flight.CurrentPassengers++;
            await _db.SaveChangesAsync();
            return RedirectToAction("Success", new { id = booking.BookingId });

        }

        [HttpGet("SuccessBooking/{id:int}")]
        public async Task<IActionResult> Success(int id)
        {
            // Retrieve the booking from the database
            var booking = await _db.Bookings.Include(b => b.User)
                                      .Include(b => b.Flight)
                                      .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }
    }

}
