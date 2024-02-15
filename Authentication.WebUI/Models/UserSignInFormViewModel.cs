using System.ComponentModel.DataAnnotations;

namespace Authentication.WebUI.Models
{
    public class UserSignInFormViewModel
    {
        [Required(ErrorMessage = "{0} gerekli")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} gerekli")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
    }
}
