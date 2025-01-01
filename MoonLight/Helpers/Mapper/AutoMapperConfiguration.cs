using MoonLight.API.Helpers.Mapper.Profiles;

namespace MoonLight.API.Helpers.Mapper
{
    public static class AutoMapperConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(AutoMapperConfiguration),
                typeof(ProductMappingProfile),
                typeof(AddressMappingProfile),
                typeof(BasketItemMappingProfile),
                typeof(CustomerBasketMappingProfile),
                typeof(OrderAddressMappingProfile),
                typeof(OrderMappingProfile)
                );
        }
    }
}
