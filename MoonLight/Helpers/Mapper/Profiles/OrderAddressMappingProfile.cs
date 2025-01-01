using AutoMapper;
using Core.Entities.OrderAgregates;
using MoonLight.API.DTOs;

namespace MoonLight.API.Helpers.Mapper.Profiles
{
    public class OrderAddressMappingProfile : Profile
    {
        public OrderAddressMappingProfile()
        {
            CreateMap<AddressDTO, OrderAddress>().ReverseMap();
        }
    }
}
