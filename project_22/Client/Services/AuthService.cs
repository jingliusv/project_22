using project_22.Shared.Form;
using System.Security.Claims;

namespace project_22.Client.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegisterForm form);
        Task<ServiceResponse<string>> Login(UserLoginForm form);
        Task<bool> IsUserAuthenticated();
        Task<string> GetClaimsApiKey();
    }
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _authStateProvider = authStateProvider ?? throw new ArgumentNullException(nameof(authStateProvider));
        }

        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity!.IsAuthenticated;
        }

        public async Task<ServiceResponse<string>> Login(UserLoginForm form)
        {
            var result = await _http.PostAsJsonAsync("api/Auth/login", form);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserRegisterForm form)
        {
            var result = await _http.PostAsJsonAsync("api/Auth/register", form);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        public async Task<string> GetClaimsApiKey()
        {
            IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();
            string apiKey = string.Empty;
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
                _claims = user.Claims;

            foreach (var claim in _claims)
                if (claim.Type == "key")
                    apiKey = claim.Value;

            return apiKey;
        }
    }
}
