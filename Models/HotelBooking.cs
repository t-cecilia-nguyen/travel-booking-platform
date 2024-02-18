using GBC_Travel_Group_90.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_90.Models
{
    public class HotelBooking
    {
        public int HotelBookingId { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; }
		[Required]
		public DateTime CheckInDate { get; set; }
		[Required]
		public DateTime CheckOutDate { get; set; }
        public enum Status
        {
            Confirmed,
            Pending,
            Canceled
        }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set;}


    }
}
