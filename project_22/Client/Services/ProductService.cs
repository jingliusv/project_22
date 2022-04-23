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
        private readonly IAuthService _authService;
        private readonly NavigationManager _navigationManager;

        public ProductService(HttpClient http, IAuthService authService, NavigationManager navigationManager)
        {
            _http = http;
            _authService = authService;
            _navigationManager = navigationManager;
        }


        public List<Product> Products { get; set; } = new List<Product>();

        public async Task<ServiceResponse<Product>> CreateProduct(AddProductForm form)
        {     
            if(await _authService.IsUserAuthenticated())
            {
                var result = await _http.PostAsJsonAsync("api/Products", form);
                return await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>();
            }
            else
            {
                _navigationManager.NavigateTo("login");
                return new ServiceResponse<Product> { Data = null };
            }
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
