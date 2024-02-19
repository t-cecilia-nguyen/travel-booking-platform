using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_90.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        [Required]
        public string HotelName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int NumberOfRooms { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
