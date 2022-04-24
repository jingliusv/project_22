using Microsoft.AspNetCore.Components;

namespace project_22.Client.Services
{
    public interface IOrderService
    {
        Task PlaceOrder();
        Task<List<OrderResponse>> GetOrders();
    }
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;
        private readonly NavigationManager _navigationManager;

        public OrderService(HttpClient http, IAuthService authService, NavigationManager navigationManager)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
        }


        public async Task PlaceOrder()
        {
            if(await _authService.IsUserAuthenticated())
            {
                var apiKey = await _authService.GetClaimsApiKey();
                await _http.PostAsync("api/orders?key=" + apiKey, null);
            }
            else
            {
                _navigationManager.NavigateTo("login");
            }
        }

        public async Task<List<OrderResponse>> GetOrders()
        {
            var apiKey = await _authService.GetClaimsApiKey();
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<OrderResponse>>>("api/orders?key=" + apiKey);
            return result.Data;
        }
    }
}
