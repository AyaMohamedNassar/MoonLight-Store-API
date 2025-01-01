using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace MoonLight.API.Errors
{
    public class ApiResponse
    {
        [JsonPropertyOrder(1)]
        public int StatusCode { get; set; }
        [JsonPropertyOrder(2)]
        public string? Message { get; set; }

        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefultMessageForStutusCode(statusCode);
        }

        private string? GetDefultMessageForStutusCode(int stutusCode)
        {
            return stutusCode switch
            {
                400 => "A bad request,You Have made",
                401 => "authorized, You are not",
                404 => "Resource wasn't Found",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anguer " +
                "leads to hate. Hate leads to...",
                _ => null,
            };
        }
    }
}
