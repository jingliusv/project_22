using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace project_22.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("products")]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> GetCartResponse(List<CartItem> cartItems)
        {
            var result = await _cartService.GetCartProductsAsync(cartItems);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> StoreCartResponse(List<CartItem> cartItems)
        {
            var result = await _cartService.StoreCartProductsAsync(cartItems);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddToCart(CartItem cartItem)
        {
            var result = await _cartService.AddToCartAsync(cartItem);
            return Ok(result);
        }

        [HttpGet("count")]
        public async Task<ActionResult<ServiceResponse<int>>> GetCartItemsCount()
        {
            return await _cartService.GetCartItemsCount();
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> GetCartItemFromDb()
        {
            return Ok(await _cartService.GetCartItemsFromDbAsync());    
        }

        [HttpPut("update-qty")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateCartQty(CartItem cartItem)
        {
            return Ok(await _cartService.UpdateQtyAsync(cartItem));
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteCartItem(int productId)
        {
            return Ok(await _cartService.RemoveCartItemAsync(productId));
        }
    }
}
