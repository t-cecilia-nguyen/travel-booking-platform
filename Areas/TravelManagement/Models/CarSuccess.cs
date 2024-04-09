namespace GBC_Travel_Group_90.Areas.TravelManagement.Models
{
    public class CarSuccess
    {
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? User { get; set; }
        public CarRental CarRental { get; set; }
    }
}
