using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_90.Areas.TravelManagement.Models
{
    public class HotelReview
    {
        public int HotelReviewId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
        public string? Content { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        public int HotelId { get; set; }

        public Hotel? Hotel { get; set; }
    }
}
