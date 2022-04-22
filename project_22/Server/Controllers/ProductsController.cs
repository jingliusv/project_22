using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_22.Shared.Form;

namespace project_22.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Product>>> CreateProductAsync(AddProductForm product)
        {
            return Ok(await _productService.CreateAsync(product));
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
        {
            return Ok(await _productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductById(int id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }
    }
}
