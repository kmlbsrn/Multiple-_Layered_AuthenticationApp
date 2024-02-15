using System.ComponentModel.DataAnnotations;

namespace Authentication.WebUI.Models
{
    public class SignUpFromViewModel
    {
        [MaxLength(50)]
        [Required]
        public string Email { get; set; }

        [MaxLength(25)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(25)]
        [Required]
        public string LastName { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        [Required]
        [MinLength(5)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set;}
    }
}
