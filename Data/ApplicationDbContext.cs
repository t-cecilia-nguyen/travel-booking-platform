﻿using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_90.Models;

namespace GBC_Travel_Group_90.Data
{
    public class ApplicationDbContext : DbContext
    {
        internal readonly object Bookings;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
            
        }

		public DbSet<Flight> Flights { get; set; }
		public DbSet<CarRental> CarRentals { get; set; }
		public DbSet<Hotel> Hotels { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Booking> Bookings { get; set; }
        public DbSet<HotelBooking> HotelBookings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
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
        }
    }
}
