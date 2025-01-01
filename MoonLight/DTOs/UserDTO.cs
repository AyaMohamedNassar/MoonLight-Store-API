namespace MoonLight.API.DTOs
{
    public class UserDTO
    {
        public required string Email {  get; set; }
        public required string DisplayName {  get; set; }
        public required string Token { get; set; }
        public string? Confirm { get; set; }

    }
}
