using AuthProject.Helpers;
using AuthProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthControlller : Controller
    {
        private readonly IAuthService _authService;
        public AuthControlller(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Something Went Wrong");
            var result = await _authService.Register(registerModel);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }
        [HttpPost("get-token")]
        public async Task<IActionResult> LogIn(LogInModel logInModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Something Went Wrong");
            var result = await _authService.LogIn(logInModel);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }
        [HttpPost("Add-Role")]
        public async Task<IActionResult> AddRole(RoleModel roleModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Something Went Wrong");
            var result = await _authService.AddRole(roleModel);
            if (!string.IsNullOrEmpty(result))
                return Ok(result);
            return BadRequest();
        }
    }
}
