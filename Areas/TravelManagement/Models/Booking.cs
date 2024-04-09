using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public string? ApplicationUserId { get; set; }

        public ApplicationUser? User { get; set; }

        public int? FlightId { get; set; }

        public Flight? Flight { get; set; }
    }
}
