using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_90.Models
{
    public class CarRental
    {
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
	}
}
