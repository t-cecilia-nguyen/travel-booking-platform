using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_90.Models
{
    public class Booking
    {
        public int BookingId {  get; set; }
        [Required]
        public required User User { get; set; }
        public Flight? Flight { get; set; }
        public Hotel? Hotel { get; set; }
        public CarRental? CarRental { get; set; }
        public DateTime BookingDate { get; set; }
        public string? Status { get; set; }
    }
}
