using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? UserId { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public byte[]? ProfilePicture { get; set; }

        public List<Booking>? Bookings { get; set; }

        public List<HotelBooking>? HotelBookings { get; set; }

        public List<CarSuccess>? CarSuccesses { get; set; }
        public int FrequentFlyerPoints { get; set; }
        public int FrequentCarPoints { get; set; }
        public int HotelLoyaltyPoints { get; set; }

    }
}
