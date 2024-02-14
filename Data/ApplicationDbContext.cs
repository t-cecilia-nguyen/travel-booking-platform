using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_90.Models;

namespace GBC_Travel_Group_90.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
            
        }

        public DbSet<Flight> Flights { get; set; }
    }
}
