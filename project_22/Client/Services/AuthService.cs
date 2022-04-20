using project_22.Shared.Form;

namespace project_22.Client.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegisterForm form);
        Task<ServiceResponse<string>> Login(UserLoginForm form);
    }
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
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
    }
}
