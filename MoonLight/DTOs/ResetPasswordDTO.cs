using System.ComponentModel.DataAnnotations;

namespace MoonLight.API.DTOs
{
    public class ResetPasswordDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "The two passwords Did not match")]
        public string ConfirmNewPassword { get; set; }
    }
}
