using System.ComponentModel.DataAnnotations;

namespace MoonLight.API.DTOs
{
    public class ForgetPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string ClientURL { get; set; }
    }
}
