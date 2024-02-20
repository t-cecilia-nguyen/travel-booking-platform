using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_90.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Location { get; set; }
        [Required]
        [Range(1, 5)]
        public int StarRate { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int NumberOfRooms { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }
        public virtual ICollection<Booking>? Bookings { get; set; }

    }
}
