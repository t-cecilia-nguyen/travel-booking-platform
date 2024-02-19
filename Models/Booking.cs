﻿using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_90.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        [Required]
        public int UserId { get; set; }
        public required User User { get; set; }
        public int FlightId { get; set; }
        public Flight? Flight { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public int CarRentalId { get; set; }
        public CarRental? CarRental { get; set; }
    }
}
