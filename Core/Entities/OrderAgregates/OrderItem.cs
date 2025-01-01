namespace Core.Entities.OrderAgregates
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrdered item, decimal price, int quantity)
        {
            OrderedItem = item;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrdered OrderedItem {  get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
