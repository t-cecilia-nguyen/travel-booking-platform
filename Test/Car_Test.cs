using GBC_Travel_Group_90.Areas.TravelManagement.Controllers;
using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.Data;
using GBC_Travel_Group_90.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class CarRentalControllerTests
    {
        [Fact]
        public async Task Create_ValidCarRental_ReturnsRedirectToActionResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(new ApplicationUser());

            var mockLogger = Mock.Of<ILogger<CarRentalController>>();

            var mockValidateModelFilter = new Mock<ValidateModelFilter>();

            using (var dbContext = new ApplicationDbContext(options))
            {
                var carRental = new CarRental
                {
                    RentalCompany = "Company",
                    PickUpLocation = "Location",
                    CarModel = "Model",
                    MaxPassengers = 5,
                    Price = 100
                };
                dbContext.CarRentals.Add(carRental);
                dbContext.SaveChanges();
            }

            var controller = new CarRentalController(new ApplicationDbContext(options), mockUserManager.Object, mockLogger);

            // Act
            var result = await controller.Create(new CarRental
            {
                RentalCompany = "Company",
                PickUpLocation = "Location",
                CarModel = "Model", 
                MaxPassengers = 5, 
                Price = 100 
            });

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task BookCarRental_ValidCarRental_ReturnsRedirectToActionResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(new ApplicationUser());

            var mockLogger = Mock.Of<ILogger<CarRentalController>>(); 

            var mockValidateModelFilter = new Mock<ValidateModelFilter>();

            using (var dbContext = new ApplicationDbContext(options))
            {
                var carRental = new CarRental
                {
                    RentalCompany = "Company",
                    PickUpLocation = "Location",
                    CarModel = "Model",
                    MaxPassengers = 5,
                    Price = 100
                };
                dbContext.CarRentals.Add(carRental);
                dbContext.SaveChanges();
            }

            var controller = new CarRentalController(new ApplicationDbContext(options), mockUserManager.Object, mockLogger);


            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "Admin"),
                        new Claim(ClaimTypes.NameIdentifier, "Password1!@"),
                    }, "mock"))
                }
            };

            // Act
            var result = await controller.Book(1); // Assuming carRentalId 1 exists in the in-memory database

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Success", redirectToActionResult.ActionName);
        }



    }
}