using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_90.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string? LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
