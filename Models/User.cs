using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_90.Models
{
    public abstract class User
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }

    public class RegisteredUser : User
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
    }

    public class GuestUser : User
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
