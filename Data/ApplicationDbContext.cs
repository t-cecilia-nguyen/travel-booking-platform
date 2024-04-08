using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_90.Models;
using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GBC_Travel_Group_90.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<CarRental> CarRentals { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<HotelBooking> HotelBookings { get; set; }
        public DbSet<CarRentalReview> CarRentalReviews { get; set; }
        public DbSet<HotelReview> HotelReviews { get; set; }
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>()
                .Property(f => f.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CarRental>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Hotel>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);

            // Seed Flight Data
            modelBuilder.Entity<Flight>().HasData(
                new Flight
                {
                    FlightId = 1,
                    FlightNumber = "F8 1602",
                    Airline = "Flair Airlines",
                    Origin = "Toronto",
                    Destination = "Fort Lauderdale",
                    DepartureTime = new DateTime(2024, 4, 15, 6, 30, 0),
                    ArrivalTime = new DateTime(2024, 4, 15, 9, 50, 0),
                    Price = 143.00m,
                    MaxPassengers = 4
                },
                new Flight
                {
                    FlightId = 2,
                    FlightNumber = "AC 9",
                    Airline = "Air Canada",
                    Origin = "Toronto",
                    Destination = "Hanoi",
                    DepartureTime = new DateTime(2024, 4, 15, 13, 30, 0),
                    ArrivalTime = new DateTime(2024, 4, 16, 22, 15, 0),
                    Price = 1693.00m,
                    MaxPassengers = 4
                },
                new Flight
                {
                    FlightId = 3,
                    FlightNumber = "AC 7952",
                    Airline = "Air Canada",
                    Origin = "Toronto",
                    Destination = "Montreal",
                    DepartureTime = new DateTime(2024, 4, 15, 7, 45, 0),
                    ArrivalTime = new DateTime(2024, 4, 15, 8, 40, 0),
                    Price = 434.00m,
                    MaxPassengers = 2
                },
                new Flight
                {
                    FlightId = 4,
                    FlightNumber = "PD 372",
                    Airline = "Porter Airlines",
                    Origin = "Vancouver",
                    Destination = "Montreal",
                    DepartureTime = new DateTime(2024, 4, 15, 7, 55, 0),
                    ArrivalTime = new DateTime(2024, 4, 15, 15, 50, 0),
                    Price = 359.00m,
                    MaxPassengers = 3
                },
                new Flight
                {
                    FlightId = 5,
                    FlightNumber = "AC 890",
                    Airline = "Air Canada",
                    Origin = "Toronto",
                    Destination = "Rome",
                    DepartureTime = new DateTime(2024, 4, 22, 20, 30, 0),
                    ArrivalTime = new DateTime(2024, 4, 23, 11, 05, 0),
                    Price = 866.00m,
                    MaxPassengers = 4
                });

            // Seed Hotel data
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    HotelId = 1,
                    Name = "Marriot",
                    Location = "123 str Toronto ON",
                    StarRate = 4,
                    NumberOfRooms = 50,
                    Price = 350.00m
                },
                new Hotel
                {
                    HotelId = 2,
                    Name = "Holiday Inn",
                    Location = "234 str Calgary AB",
                    StarRate = 3,
                    NumberOfRooms = 100,
                    Price = 250.00m
                },
                 new Hotel
                 {
                     HotelId = 3,
                     Name = "Happy Hotel",
                     Location = "999 str Vancouver BC",
                     StarRate = 3,
                     NumberOfRooms = 20,
                     Price = 150.00m
                 },
                  new Hotel
                  {
                      HotelId = 4,
                      Name = "Angry Hotel",
                      Location = "012 str Norway",
                      StarRate = 5,
                      NumberOfRooms = 15,
                      Price = 1000.00m
                  });

            // Seed Car data
            modelBuilder.Entity<CarRental>().HasData(
                new CarRental
                {
                    CarRentalId = 1,
                    RentalCompany = "Big Company",
                    PickUpLocation = "123 str Toronto ON",
                    PickUpDate = DateTime.Now.AddDays(22), 
                    DropOffDate = DateTime.Now.AddDays(73), 
                    CarModel = "Cool Car",
                    Price = 350.00m
                },
                new CarRental
                {
                    CarRentalId = 2,
                    RentalCompany = "Rent-A-Car",
                    PickUpLocation = "456 Main St, Vancouver",
                    PickUpDate = DateTime.Now.AddDays(2),
                    DropOffDate = DateTime.Now.AddDays(7), 
                    CarModel = "SUV",
                    Price = 500.00m
                },
                new CarRental
                {
                    CarRentalId = 3,
                    RentalCompany = "City Cars",
                    PickUpLocation = "789 Elm St, Calgary",
                    PickUpDate = DateTime.Now.AddDays(3), // Example pickup date
                    DropOffDate = DateTime.Now.AddDays(6), // Example drop-off date
                    CarModel = "Compact",
                    Price = 250.00m
                });

            // Seed User data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@example.com",
                    IsAdmin = true
                }
            );

        }*/
    }
}