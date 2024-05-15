using E_Commerce.API.Errors;
using E_Commerce.API.Helper;
using E_Commerce.core.DataTransferObjects;
using E_Commerce.core.Interfaces.Services;
using E_Commerce.core.Spcifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        [Cash(60)]
        //[FromRoute] [FromQuery]//
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpcificationParameters specs) 
            => Ok(await _service.GetAllProductAsync(specs));

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var result = await _service.GetProductAsync(id);
            return result is not null ? Ok(result) :NotFound( new ApiResponse(404,$"the product with id = {id} not found"));
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandTypeDto>>> GetProductsBrands() 
            => Ok(await _service.GetAllBrandsAsync());
        
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<BrandTypeDto>>> GetProductsTypes() 
            => Ok(await _service.GetAllTypesAsync());
    }
}
