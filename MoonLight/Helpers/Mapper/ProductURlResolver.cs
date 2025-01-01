using AutoMapper;
using Core.Entities;
using MoonLight.API.DTOs;

namespace MoonLight.API.Helpers.Mapper
{
    public class ProductURlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _config;

        public ProductURlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiURl"] + "Images/Products/" + source.PictureUrl;
            }

            return null;
        }
    }
}
