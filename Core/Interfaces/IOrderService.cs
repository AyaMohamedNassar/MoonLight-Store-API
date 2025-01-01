using Core.Entities.OrderAgregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId,
            OrderAddress shippingAddress);
        Task<Order> GetOrderByIdAsync(int orderId, string buyerEmail);
        Task<IReadOnlyList<Order>> GetUserOrdersAsync(string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
