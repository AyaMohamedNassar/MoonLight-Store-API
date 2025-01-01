using Core.Entities.RedisEntities;

namespace Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketsAsync(string basketId);
        Task<CustomerBasket> UpdateBasketsAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
