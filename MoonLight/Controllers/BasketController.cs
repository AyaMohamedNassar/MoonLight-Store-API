using AutoMapper;
using Core.Entities.RedisEntities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MoonLight.API.DTOs;

namespace MoonLight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public BasketController(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id) 
        {
            return Ok(await _basketRepository.GetBasketsAsync(id) ?? new CustomerBasket(id));
        }

        [HttpPost("Update")]
        public async Task<ActionResult<CustomerBasketDTO>> UpdateBasket(CustomerBasketDTO basket)
        {
            CustomerBasket customerBasket = _mapper.Map<CustomerBasket>(basket);

            var updateBasket = await _basketRepository.UpdateBasketsAsync(customerBasket);

            return Ok(updateBasket);
        }

        [HttpDelete("Delete/{id}")]
        public async Task DeleteBasketAsync(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
