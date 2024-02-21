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
        public List<HotelBooking>? HotelBookings { get; set; }

        // Check if the hotel is available for the specified date range
        public bool IsAvailableForDates(DateTime? checkInDate, DateTime? checkOutDate, List<HotelBooking> bookingsForDateRange)
        {
            if (HotelBookings == null)
                return true; // No bookings, so hotel is available

            // Check if any booked reservation overlaps with the requested date range
            var overlappingReservation = HotelBookings.FirstOrDefault(reservation =>
                bookingsForDateRange.Any(b => IsDateRangeOverlap(checkInDate, checkOutDate, b.CheckInDate, b.CheckOutDate)));

            // If any overlapping reservation is found, hotel is not available
            return overlappingReservation == null;
        }

        // Helper method to check if two date ranges overlap
        private bool IsDateRangeOverlap(DateTime? start1, DateTime? end1, DateTime? start2, DateTime? end2)
        {
            return start1 < end2 && start2 < end1;
        }

    }

}