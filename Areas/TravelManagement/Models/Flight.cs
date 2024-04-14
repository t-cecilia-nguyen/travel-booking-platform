using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Models
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }

        [Required]
        [Display(Name = "Flight Number")]
        [StringLength(10)]
        public string FlightNumber { get; set; }

        [Required]
        [Display(Name = "Airline")]
        [StringLength(30)]
        public string Airline { get; set; }

        [Required]
        [Display(Name = "Origin")]
        [StringLength(30)]
        public string Origin { get; set; }

        [Required]
        [Display(Name = "Destination")]
        [StringLength(30)]
        public string Destination { get; set; }

        [Required]
        [Display(Name = "Departure Date/Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DepartureTime { get; set; }

        [Required]
        [Display(Name = "Arrival Date/Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ArrivalTime { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Max Passengers")]
        public int MaxPassengers { get; set; }

        [Display(Name = "Current Passengers")]
        public int CurrentPassengers { get; set; }

     
        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}
