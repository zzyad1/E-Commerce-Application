using E_Commerce.core.DataTransferObjects;
using E_Commerce.core.Entities.Order;
using E_Commerce.core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> Create(OrderDto orderDto)
        {
            var order = await _orderService.CreateOrdersAsync(orderDto);
            return Ok(order);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order =_orderService.GetAllOrdersAsync(email);
            return Ok(order);
        }
        
        [Authorize]
        [HttpGet("id")]
        public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetOrderById(Guid id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order =_orderService.GetOrdersAsync(id ,email);
            return Ok(order);
        }

        [HttpGet("GetDeliveryMethod")]
        public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetDeliveryMethod()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }
    }
}
