using Core.Entities;
using Core.Entities.OrderAgregates;
using Core.Interfaces;
using Core.Specifications.OrderSpec;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, OrderAddress shippingAddress)
        {
            var basket = await _basketRepository.GetBasketsAsync(basketId);

            var items = new List<OrderItem>();

            foreach (var item in basket.Items) 
            {
                Product productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                ProductItemOrdered itemOrdered = new (productItem.Id, productItem.Name, productItem.PictureUrl);
                OrderItem orderItem = new(itemOrdered, productItem.Price, item.Quantity);

                items.Add(orderItem);
            }

            DeliveryMethod deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>()
                .GetByIdAsync(deliveryMethodId);

            decimal subTotal = items.Sum(item =>  item.Quantity * item.Price);

            Order order = new(items, buyerEmail,shippingAddress,deliveryMethod,subTotal);
            
            _unitOfWork.Repository<Order>().Add(order);
            
            var result = await _unitOfWork.SaveAsync();

            if (result <= 0) return null;

            await _basketRepository.DeleteBasketAsync(basketId);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId, string buyerEmail)
        {
            OrdersWithItemsAndSortingSpec spec = new(orderId, buyerEmail);

            return await _unitOfWork.Repository<Order>().GetEntity(spec);
        }

        public async Task<IReadOnlyList<Order>> GetUserOrdersAsync(string buyerEmail)
        {
            OrdersWithItemsAndSortingSpec spec = new(buyerEmail);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}
