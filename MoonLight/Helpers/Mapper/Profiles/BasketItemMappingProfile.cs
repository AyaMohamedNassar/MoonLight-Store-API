using AutoMapper;
using Core.Entities.RedisEntities;
using MoonLight.API.DTOs;

namespace MoonLight.API.Helpers.Mapper.Profiles
{
    public class BasketItemMappingProfile : Profile
    {
        public BasketItemMappingProfile()
        {
            CreateMap<BasketItem, BasketItemDTO>()
                .ReverseMap();
        }
    }
}
