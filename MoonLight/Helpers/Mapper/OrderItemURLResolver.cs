using AutoMapper;
using Core.Entities.OrderAgregates;
using MoonLight.API.DTOs;

namespace MoonLight.API.Helpers.Mapper
{
    public class OrderItemURLResolver : IValueResolver<OrderItem, FinalOrderItemDTO, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemURLResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, FinalOrderItemDTO destination, string destMember,
            ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.OrderedItem.PictureURL)) { 
                return _configuration["ApiURl"] + "Images/Products/" + source.OrderedItem.PictureURL;
            }

            return null;
        }
    }
}
