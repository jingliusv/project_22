using Microsoft.AspNetCore.Components;
using project_22.Shared.Form;

namespace project_22.Client.Services
{
    public interface IProductService
    {
        List<Product> Products { get; set; }
        Task GetProducts();
        Task<ServiceResponse<Product>> GetProductById(int productId);
        Task<ServiceResponse<Product>> CreateProduct(AddProductForm form);
    }
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;

        public ProductService(HttpClient http, AuthenticationStateProvider authStateProvider, NavigationManager navigationManager)
        {
            _http = http;
            _authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
        }

        private async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public List<Product> Products { get; set; } = new List<Product>();

        public async Task<ServiceResponse<Product>> CreateProduct(AddProductForm form)
        {     
            var result = await _http.PostAsJsonAsync("api/Products", form);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>();
        }

        public async Task<ServiceResponse<Product>> GetProductById(int productId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/products/{productId}");
            return result!;
        }

        public async Task GetProducts()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/products");
            if(result != null && result.Data != null)
                Products = result.Data;
        }
    }
}
