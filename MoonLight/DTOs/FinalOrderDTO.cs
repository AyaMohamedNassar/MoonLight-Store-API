using Core.Entities.OrderAgregates;
using Core.Enumerations;

namespace MoonLight.API.DTOs
{
    public class FinalOrderDTO
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public OrderAddress ShipToAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public IReadOnlyList<FinalOrderItemDTO> Items { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } 
    }
}
