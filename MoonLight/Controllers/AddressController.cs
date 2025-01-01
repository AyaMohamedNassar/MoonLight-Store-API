using AutoMapper;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoonLight.API.DTOs;
using MoonLight.API.Errors;
using MoonLight.API.Extentions;

namespace MoonLight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AddressController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            var user = await _userManager.GetUserWithAdsressAsync(HttpContext.User);

            var addressDTO = _mapper.Map<AddressDTO>(user.Address);
            return Ok(addressDTO);
        }

        [Authorize]
        [HttpPut("Update")]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO addressDTO)
        {
            var user = await _userManager.GetUserWithAdsressAsync(HttpContext.User);

            user.Address = _mapper.Map<Address>(addressDTO);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400, "Problem while Updating the user address")); 
            }

            return Ok(addressDTO);
        }
    }
}
