using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_90.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<CarRental> CarRentals { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<HotelBooking> HotelBookings { get; set; }
        public DbSet<CarSuccess> CarSuccess { get; set; }
        public DbSet<CarRentalReview> CarRentalReviews { get; set; }
        public DbSet<HotelReview> HotelReviews { get; set; }
    }
}