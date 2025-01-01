using AutoMapper;
using Core.Entities;
using MoonLight.API.DTOs;

namespace MoonLight.API.Helpers.Mapper.Profiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductURlResolver>());
        }
    }
}
