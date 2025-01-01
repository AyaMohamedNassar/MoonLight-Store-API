using System.ComponentModel.DataAnnotations;

namespace MoonLight.API.DTOs
{
    public class AddressDTO
    {
        [Required]
        public string Country { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        public string? ZipCode { get; set; }
        public string? UserId { get; set; }
    }
}
