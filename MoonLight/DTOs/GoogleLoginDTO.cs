namespace MoonLight.API.DTOs
{
    public class GoogleLoginDTO
    {
        public required string IdToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
