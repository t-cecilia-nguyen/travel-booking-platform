using GBC_Travel_Group_90.Areas.TravelManagement.Controllers;
using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class FlightControllerTests
    {
        [Fact]
        public async Task Create_ValidFlight_ReturnsRedirectToActionResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Create and seed the in-memory database
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Flights.Add(new Flight
                {
                    FlightNumber = "ABC123",
                    Airline = "Test Airline",
                    Origin = "Origin",
                    Destination = "Destination",
                    DepartureTime = DateTime.Now,
                    ArrivalTime = DateTime.Now.AddHours(2),
                    Price = 200,
                    MaxPassengers = 100,
                    CurrentPassengers = 0 // Assuming no passengers initially
                });
                dbContext.SaveChanges();
            }

            // Mock UserManager<ApplicationUser>
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            // Set up a mock user
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "Admin"),
                new Claim(ClaimTypes.NameIdentifier, "Password1!@"),
                // Add any other claims as needed
            }, "mock"));

            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .Returns(Task.FromResult(new ApplicationUser { /* Add required properties */ }));

            var controller = new FlightController(new ApplicationDbContext(options))
            {
                UserManager = mockUserManager.Object
            };

            // Set the HttpContext for the controller
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await controller.Create(new Flight
            {
                FlightNumber = "XYZ456",
                Airline = "Another Airline",
                Origin = "Origin",
                Destination = "Destination",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(3),
                Price = 250,
                MaxPassengers = 120,
                CurrentPassengers = 0 // Assuming no passengers initially
            });

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfFlights()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Seed the in-memory database with flights
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Flights.AddRange(new List<Flight>
                {
                    new Flight
                    {
                        FlightNumber = "ABC123",
                        Airline = "Test Airline",
                        Origin = "Origin",
                        Destination = "Destination",
                        DepartureTime = DateTime.Now,
                        ArrivalTime = DateTime.Now.AddHours(2),
                        Price = 200,
                        MaxPassengers = 100,
                        CurrentPassengers = 0 // Assuming no passengers initially
                    },
                    new Flight
                    {
                        FlightNumber = "XYZ456",
                        Airline = "Another Airline",
                        Origin = "Origin",
                        Destination = "Destination",
                        DepartureTime = DateTime.Now,
                        ArrivalTime = DateTime.Now.AddHours(3),
                        Price = 250,
                        MaxPassengers = 120,
                        CurrentPassengers = 0 // Assuming no passengers initially
                    }
                });
                dbContext.SaveChanges();
            }

            // Act
            IActionResult actionResult;
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new FlightController(dbContext);


                actionResult = await controller.Index();
            }

            // Assert
            var viewResult = Assert.IsType<ViewResult>(actionResult);
            var model = Assert.IsAssignableFrom<List<Flight>>(viewResult.Model);
            Assert.Equal(2, model.Count); // Ensure that all flights are retrieved
        }

    }
}