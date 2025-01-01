using System.ComponentModel.DataAnnotations;

namespace MoonLight.API.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public  string Email {  get; set; }
        [Required]
        public  string DisplayName {  get; set; }
        [Required]
        public  string UserName {  get; set; }
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", 
            ErrorMessage = "Ensures at least one lowercase letter. " +
            "Ensures at least one uppercase letter. Ensures at least one digit. " +
            "Ensures at least one special character. A Password that is at least 8 characters long.")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string? PhoneNumber {  get; set; }
    }
}
