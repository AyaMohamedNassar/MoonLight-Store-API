using Core.Enumerations;

namespace Core.Entities.OrderAgregates
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order(IReadOnlyList<OrderItem> items, string buyerEmail, OrderAddress shipToAddress,
            DeliveryMethod deliveryMethod, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderAddress ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; } 
        public IReadOnlyList<OrderItem> Items { get; set; }
        public decimal SubTotal {  get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string? PaymentIntentId {  get; set; }

        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Price;
        }

    }
}
