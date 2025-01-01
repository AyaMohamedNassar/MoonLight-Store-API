namespace MoonLight.API.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required string PictureUrl { get; set; }
        public int CategoryId { get; set; }
        public required string Category { get; set; }
        public int BrandId { get; set; }
        public required string Brand { get; set; }
    }
}
