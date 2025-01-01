using Core.Entities.RedisEntities;

namespace MoonLight.API.DTOs
{
    public class CustomerBasketDTO
    {
        public required string Id { get; set; }
        public required List<BasketItemDTO> Items { get; set; } 
    }
}
