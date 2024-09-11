using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
    public class RegesterDto
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string  Email { get; set; }

        [Required]
        [Phone]
        public string  PhoneNumber { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$" , 
            ErrorMessage = "Password must Contain : 1 UpperCase , 1 LowerCase , 1 Digit , 1 Spicial Character")]
        public string Password { get; set; }
    }
}
