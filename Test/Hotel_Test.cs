using GBC_Travel_Group_90.Areas.TravelManagement.Controllers;
using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Security.Claims;
using Xunit;

namespace Test
{
    public class HotelControllerTests
    {
        private readonly IServiceProvider _serviceProvider;

        public HotelControllerTests()
        {
            // Set up an in-memory database for testing
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));
            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void Create_ReturnsViewResult()
        {
            // Arrange
            using var context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            var controller = new HotelController(context);

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

    }
}