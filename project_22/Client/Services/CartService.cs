using Blazored.LocalStorage;

namespace project_22.Client.Services
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(CartItem cartItem);
        Task<List<CartItem>> GetAllCartItems();
        Task<List<CartProductResponse>> GetAllCartProducts();
        Task RemoveCartProduct(int productId);
        Task UpdateQty(CartProductResponse product);
    }
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public CartService(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }

        public event Action OnChange;

        public async Task AddToCart(CartItem cartItem)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if(cart == null)
            {
                cart = new List<CartItem>();
            }
            
            var sameItem = cart.Find(c => c.ProductId == cartItem.ProductId);
            if(sameItem == null)
            {
                cart.Add(cartItem);
            }
            else
            {
                sameItem.Qty += cartItem.Qty;
            }

            await _localStorage.SetItemAsync("cart", cart);
            OnChange.Invoke();
        }

        public async Task<List<CartItem>> GetAllCartItems()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if( cart == null)
            {
                cart= new List<CartItem>();
            }

            return cart;
        }

        public async Task<List<CartProductResponse>> GetAllCartProducts()
        {
            var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            var response = await _http.PostAsJsonAsync("api/Carts/products", cartItems);
            var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();
            return cartProducts.Data;
        }

        public async Task RemoveCartProduct(int productId)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
                return;

            var cartItem = cart.Find(c => c.ProductId == productId);

            if(cartItem != null)
            {
                cart.Remove(cartItem);
                await _localStorage.SetItemAsync("cart", cart);
                OnChange.Invoke();
            }
                
        }

        public async Task UpdateQty(CartProductResponse product)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
                return;

            var cartItem = cart.Find(c => c.ProductId == product.ProductId);

            if (cartItem != null)
            {
                cartItem.Qty = product.Qty;
                await _localStorage.SetItemAsync("cart", cart);
            }
        }
    }
}
