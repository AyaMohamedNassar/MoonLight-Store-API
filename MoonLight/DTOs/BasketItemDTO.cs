using System.ComponentModel.DataAnnotations;

namespace MoonLight.API.DTOs
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.10, double.MaxValue, ErrorMessage = "Price Must be greater than zero.")]
        public decimal Price { get; set; }
        [Required]
        [Range (1, int.MaxValue, ErrorMessage = "Quantity Must be greater than zero.")]
        public int Quantity { get; set; }
        [Required]
        public string PictureURL { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
