using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Product: BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public required string PictureUrl { get; set; }
        public int CategoryId {  get; set; }
        public Category Category { get; set; }
        public required int BrandId {  get; set; }
        public Brand Brand { get; set; }

    }
}
