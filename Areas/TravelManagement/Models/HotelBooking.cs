using GBC_Travel_Group_90.Data;
using GBC_Travel_Group_90.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Models
{
    public class HotelBooking
    {
        public int HotelBookingId { get; set; }
        [Required]
        [Display(Name = "Booking Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BookingDate { get; set; }
        [Required]
        [Display(Name = "Check In Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CheckInDate { get; set; }
        [Required]
        [Display(Name = "Check Out Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CheckOutDate { get; set; }
        [Required]
        [Display(Name = "Number of rooms")]
        public int NumOfRoomsToBook { get; set; }

        public Status Status { get; set; }

        public string? ApplicationUserId { get; set; }

        public ApplicationUser? User { get; set; }

        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }





    }
    public enum Status
    {
        Confirmed,
        Pending,
        Canceled
    }

}
