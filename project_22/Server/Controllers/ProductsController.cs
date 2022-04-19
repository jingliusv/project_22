using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_22.Server.Service;
using project_22.Shared.Entity;

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

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return Ok(await _productService.GetAllAsync());
        }
    }
}
