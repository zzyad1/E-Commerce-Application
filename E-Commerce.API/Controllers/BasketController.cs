using E_Commerce.API.Errors;
using E_Commerce.core.DataTransferObjects;
using E_Commerce.core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketServices _services;

        public BasketController(IBasketServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<BasketDto>> Get(string id) 
        {
            var basket =await _services.GetBasketAsync(id);
            return basket is null ? NotFound(new ApiResponse(404 , $"Basket with {id} not found")) : Ok(basket);
        }
        
        [HttpPost]
        public async Task<ActionResult<BasketDto>> Update(BasketDto basketDto) 
        {
            var basket =await _services.UpdateBasketAsync(basketDto);
            return basket is null ? NotFound(new ApiResponse(404 , $"Basket with {basketDto.Id} not found")) : Ok(basket);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        => Ok( await _services.DeleteBasketAsync(id));
    }
}
