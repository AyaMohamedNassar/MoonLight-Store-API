using Core.Entities.OrderAgregates;

namespace MoonLight.API.DTOs
{
    public class OrderItemDTO
    {
        public ProductItemOrdered OrderedItem { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
