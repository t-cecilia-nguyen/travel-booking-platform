using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Models
{
    public class CarSuccess
    {
        [Key]
        public int CarSuccessId { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int? CarRentalId { get; set; }
        public CarRental CarRental { get; set; }
    }
}
