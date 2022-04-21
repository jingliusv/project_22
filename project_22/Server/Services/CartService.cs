using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace project_22.Server.Services
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductResponse>>> GetCartProductsAsync(List<CartItem> cartItems);
        Task<ServiceResponse<List<CartProductResponse>>> StoreCartProductsAsync(List<CartItem> cartItems);
        Task<ServiceResponse<int>> GetCartItemsCount();
        Task<ServiceResponse<List<CartProductResponse>>> GetCartItemsFromDbAsync();
        Task<ServiceResponse<bool>> AddToCartAsync(CartItem cartItem);
        Task<ServiceResponse<bool>> UpdateQtyAsync(CartItem cartItem);
        Task<ServiceResponse<bool>> RemoveCartItemAsync(int productId);
    }

    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));



        public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProductsAsync(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductResponse>> 
            {
                Data = new List<CartProductResponse>()
            };

            foreach(var item in cartItems)
            {
                var product = await _context.Products.Where(p => p.Id == item.ProductId).FirstOrDefaultAsync();

                if(product == null)
                {
                    continue;
                }

                var cartProduct = new CartProductResponse
                {
                    ProductId = product.Id,
                    ProductName = product.ProductName,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,
                    Qty = item.Qty
                };
                result.Data.Add(cartProduct);
            }

            return result;
        }

        public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartProductsAsync(List<CartItem> cartItems)
        {
            cartItems.ForEach(c => c.UserId = GetUserId());
            _context.CartItems.AddRange(cartItems);
            await _context.SaveChangesAsync();

            return await GetCartItemsFromDbAsync();
        }

        public async Task<ServiceResponse<int>> GetCartItemsCount()
        {
            var count = (await _context.CartItems.Where(c => c.UserId == GetUserId()).ToListAsync()).Count;
            return new ServiceResponse<int> { Data = count };
        }

        public async Task<ServiceResponse<List<CartProductResponse>>> GetCartItemsFromDbAsync()
        {
            return await GetCartProductsAsync(await _context.CartItems.Where(_c => _c.UserId == GetUserId()).ToListAsync());
        }

        public async Task<ServiceResponse<bool>> AddToCartAsync(CartItem cartItem)
        {
            cartItem.UserId = GetUserId();
            var sameItem = await _context.CartItems.FirstOrDefaultAsync(c => c.UserId == cartItem.UserId && c.ProductId == cartItem.ProductId);
            if(sameItem == null)
            {
                _context.CartItems.Add(cartItem);
            }
            else
            {
                sameItem.Qty += cartItem.Qty;
            }

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> UpdateQtyAsync(CartItem cartItem)
        {          
            var cartItemFromDb = await _context.CartItems.FirstOrDefaultAsync(c => c.UserId == GetUserId() && c.ProductId == cartItem.ProductId);
            if(cartItemFromDb == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Det finns inte den här produkten i varukorgen."
                };
            }
               
            
            cartItemFromDb.Qty = cartItem.Qty;
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> RemoveCartItemAsync(int productId)
        {
            var cartItemFromDb = await _context.CartItems.FirstOrDefaultAsync(c => c.UserId == GetUserId() && c.ProductId == productId);
            if (cartItemFromDb == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Det finns inte den här produkten i varukorgen."
                };
            }
            else
            {
                _context.CartItems.Remove(cartItemFromDb);
                await _context.SaveChangesAsync();
                return new ServiceResponse<bool> { Data= true };
            }
        }
    }
}
