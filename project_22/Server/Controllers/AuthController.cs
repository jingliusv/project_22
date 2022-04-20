using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_22.Shared.Entity;
using project_22.Shared.Form;

namespace project_22.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>>RegisterUser(UserRegisterForm form)
        {
            var response = await _authService.RegisterAsync(form);

            if (!response.Success) { return BadRequest(response); }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> LoginUser(UserLoginForm form)
        {
            var response = await _authService.Login(form.Email, form.Password);
            if(!response.Success)
                return BadRequest(response);

            return Ok(response);    
        }
    }
}
