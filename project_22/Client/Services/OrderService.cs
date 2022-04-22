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
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;

        public OrderService(HttpClient http, AuthenticationStateProvider authStateProvider, NavigationManager navigationManager)
        {
            _http = http;
            _authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
        }

        private async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task PlaceOrder()
        {
            if(await IsUserAuthenticated())
            {
                await _http.PostAsync("api/orders", null);
            }
            else
            {
                _navigationManager.NavigateTo("login");
            }
        }

        public async Task<List<OrderResponse>> GetOrders()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<OrderResponse>>>("api/orders");
            return result.Data;
        }
    }
}
