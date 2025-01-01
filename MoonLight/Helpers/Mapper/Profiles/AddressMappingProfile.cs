using AutoMapper;
using Core.Entities.Identity;
using MoonLight.API.DTOs;

namespace MoonLight.API.Helpers.Mapper.Profiles
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            CreateMap<Address, AddressDTO>()
                .ReverseMap();
        }
    }
}
