using GBC_Travel_Group_90.Models;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Models
{
    public class CarSuccess
    {
        public int Id { get; set; }
        public User User { get; set; }
        public CarRental CarRental { get; set; }
    }
}
