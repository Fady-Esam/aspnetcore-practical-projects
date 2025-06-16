using APIProject.Data;
using APIProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        public AccountsController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserDTO userDto)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new() 
                {
                    UserName = userDto.UserName
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, userDto.Password);
                if(result.Succeeded) return Ok(new { Result = result , State = "Success"});
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LogIn(UserDTO userDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(userDto.UserName);
                if(user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, userDto.Password))
                    {
                        var secretKey = _configuration["JwtSettings:SecretKey"];
                        var issuer = _configuration["JwtSettings:Issuer"];
                        var audience = _configuration["JwtSettings:Audience"];
                        var expiryInMinutes = int.Parse(_configuration["JwtSettings:TokenExpiryInMinutes"]);
                        var claims = new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(ClaimTypes.NameIdentifier, user.Id)
                        };

                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: issuer,
                            audience: audience,
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                            signingCredentials: creds);
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var tokenString = tokenHandler.WriteToken(token);

                        // Return the token
                        return Ok(new { Token = tokenString });
                    }
                    else 
                        return Unauthorized(new { error = "Error With Password" });
                }
            }
            ModelState.AddModelError("", "Error With UserName");
            return BadRequest(ModelState);
        }
    }
}
