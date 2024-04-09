using System.ComponentModel.DataAnnotations;
using GBC_Travel_Group_90.Models;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Models
{
    public class CarRental

    {
        public CarRental()
        {
            Available = true;
        }

        public int CarRentalId { get; set; }

        [Required]
        public string? RentalCompany { get; set; }

        [Required]
        public string? PickUpLocation { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PickUpDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DropOffDate { get; set; }

        [Required]
        public string? CarModel { get; set; }

        [Required]
        public int MaxPassengers { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }

        public string? ApplicationUserId { get; set; }

        public bool Available { get; set; }

    }
}
