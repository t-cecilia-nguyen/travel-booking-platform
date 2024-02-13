using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_90.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        [Required]
        public string FlightNumber { get; set; }
        [Required]
        public string Airline { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DepartureTime { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ArrivalTime { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
