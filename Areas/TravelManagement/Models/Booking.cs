using System.ComponentModel.DataAnnotations;
using GBC_Travel_Group_90.Models;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int? FlightId { get; set; }
        public Flight? Flight { get; set; }
        public int? HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public int? CarRentalId { get; set; }
        public CarRental? CarRental { get; set; }
    }
}
