using AutoMapper;
using Core.Entities.OrderAgregates;
using MoonLight.API.DTOs;

namespace MoonLight.API.Helpers.Mapper.Profiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, FinalOrderDTO>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.ShippingPrice, opt => opt.MapFrom(src => src.DeliveryMethod.Price));

            CreateMap<OrderItem, FinalOrderItemDTO>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.OrderedItem.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.OrderedItem.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemURLResolver>())
                .ReverseMap();
        }
    }
}
