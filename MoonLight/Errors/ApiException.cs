using System.Text.Json.Serialization;

namespace MoonLight.API.Errors
{
    public class ApiException : ApiResponse
    {
        [JsonPropertyOrder(3)]
        public string? Details { get; set; }
        public ApiException(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }
    }
}
