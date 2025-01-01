using AutoMapper;
using Core.Entities.OrderAgregates;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoonLight.API.DTOs;
using MoonLight.API.Errors;
using MoonLight.API.Extentions;

namespace MoonLight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO order)
        {
            string email = HttpContext.User?.RetriveEmail();

            OrderAddress orderAddress = _mapper.Map<OrderAddress>(order.ShipToAddress);

            Order createdOrder = await _orderService.CreateOrderAsync(email, order.DeliveryMethodId, order.BasketId, orderAddress);

            if (createdOrder == null) return BadRequest(new ApiResponse(400, "Problem Creating Order"));

            return Ok(createdOrder);
        }

        [HttpGet("All")]
        public async Task<ActionResult<IReadOnlyList<FinalOrderDTO>>> GetUserOrders()
        {
            string email = HttpContext.User.RetriveEmail();

            var orders = await _orderService.GetUserOrdersAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<FinalOrderDTO>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinalOrderDTO>> GetUserOrderById(int id)
        {
            string email = HttpContext.User.RetriveEmail();

            var order = await _orderService.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(404, "Order Not Found"));

            return Ok(_mapper.Map<FinalOrderDTO>(order));
        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<DeliveryMethod>> GetDeliveryMethods()
        {
            string email = HttpContext.User.RetriveEmail();

            var DelivaryMethod = await _orderService.GetDeliveryMethodsAsync();

            if (DelivaryMethod == null) return NotFound(new ApiResponse(404, "Delivery Method Not Found"));

            return Ok(DelivaryMethod);
        }
    }
}
