namespace GBC_Travel_Group_90.Areas.TravelManagement.Models
{
    public class AllBookings
    {
        public List<Booking> Bookings { get; set; }
        public List<CarSuccess> CarSuccesses { get; set; }
        public List<HotelBooking> HotelBookings { get; set; }
    }
}
