using AutoMapper;
using Core.Entities.RedisEntities;
using MoonLight.API.DTOs;

namespace MoonLight.API.Helpers.Mapper.Profiles
{
    public class CustomerBasketMappingProfile : Profile
    {
        public CustomerBasketMappingProfile()
        {
            CreateMap<CustomerBasket, CustomerBasketDTO>()
                .ReverseMap();
        }
    }
}
