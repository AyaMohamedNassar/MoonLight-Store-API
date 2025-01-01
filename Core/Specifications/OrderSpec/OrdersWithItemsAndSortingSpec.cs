using Core.Entities.OrderAgregates;

namespace Core.Specifications.OrderSpec
{
    public class OrdersWithItemsAndSortingSpec : BaseSpecification<Order>
    {
        public OrdersWithItemsAndSortingSpec(string email) : base(order => order.BuyerEmail == email)
        {
            AddInclude(order => order.Items);
            AddInclude(order => order.DeliveryMethod);
            AddOrderByDescending(order => order.OrderDate);
        }

        public OrdersWithItemsAndSortingSpec(int orderId, string email)
            : base(order => order.Id == orderId && order.BuyerEmail == email)
        {
            AddInclude(order => order.Items);
            AddInclude(order => order.DeliveryMethod);
        }
    }
}
