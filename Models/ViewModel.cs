namespace GBC_Travel_Group_90.Models
{
    public class ViewModel
    {
        public User User { get; set; }
        public List<Booking>? Bookings { get; set; }
        public List<CarRental>? CarRentals { get; set; }
        public List<HotelBooking>? HotelBookings { get; set; }
    }

}
