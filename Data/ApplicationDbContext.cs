using Microsoft.EntityFrameworkCore;
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

        public DbSet<CarRental> CarRentals { get; set;}

		public DbSet<User> Users { get; set; }


	}
}
