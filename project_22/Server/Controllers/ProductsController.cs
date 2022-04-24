using Microsoft.AspNetCore.Mvc;
using project_22.Server.Filters;

namespace project_22.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]   
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        [UseApiKey]
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

        [HttpPut("{id}")]
        [UseApiKey]
        public async Task<ActionResult<ServiceResponse<Product>>> UpdateProduct(UpdateProductForm form, int id)
        {
            return Ok(await _productService.UpdateAsync(form, id));
        }

        [HttpDelete("{id}")]
        [UseApiKey]
        public async Task<ActionResult<ServiceResponse<int>>> DeleteProduct(int id)
        {
            return Ok(await _productService.DeleteAsync(id));
        }
    }
}
