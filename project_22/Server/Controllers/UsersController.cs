using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace project_22.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpPut("{email}")]
        public async Task<ActionResult<ServiceResponse<string>>> UpdateUserInfo(UpdateUserForm form, string email)
        {
            return Ok(await _userService.UpdateAsync(form, email));
        }

        [HttpDelete("{email}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteUser(string email)
        {
            return Ok(await _userService.DeleteAsync(email));
        }

    }
}
