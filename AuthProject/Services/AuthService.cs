using AuthProject.Helpers;
using AuthProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging.Signing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public AuthService(UserManager<AppUser> userManager, IOptions<JWT> jwt, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }
        public async Task<AuthModel>Register(RegisterModel registerModel)
        {
            if (await _userManager.FindByEmailAsync(registerModel.Email) is not null)
                return new AuthModel { Message = "Email is already in use" };
            if (await _userManager.FindByNameAsync(registerModel.UserName) is not null)
                return new AuthModel { Message = "UserName is already in use" };
            var user = new AppUser
            {
                
                UserName = registerModel.UserName,
                Email = registerModel.Email,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
            };
            await _userManager.AddToRoleAsync(user, "User");
            var res = await _userManager.CreateAsync(user, registerModel.Password);
            if (!res.Succeeded)
                return new AuthModel { Message = string.Join(",", res.Errors.Select(i => i.Description).ToList()) };
            JwtSecurityToken securityToken = await GetToken(user);
            return new AuthModel
            {
                Message = "Registered Successfully",
                Email = user.Email,
                UserName = user.UserName,
                ExpiresOn = securityToken.ValidTo,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                IsAuthenticated = true,
            };
        }
        public async Task<JwtSecurityToken> GetToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken (
                 issuer : _jwt.Issuer,
                 audience: _jwt.Audience,
                 claims: claims,
                 expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                 signingCredentials: cred
                );
            return securityToken;
        }
        public async Task<AuthModel> LogIn(LogInModel logInModel)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(logInModel.Email);
            if (user is null)
            {
                authModel.Message = "Invalid Email";
                return authModel;
            }
            if(!await _userManager.CheckPasswordAsync(user, logInModel.Password))
            {
                authModel.Message = "Invalid Password";
                return authModel;
            }
            JwtSecurityToken securityToken = await GetToken(user);
            authModel.Message = "Logged In Successfully";
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.ExpiresOn = securityToken.ValidTo;
            authModel.Roles = (await _userManager.GetRolesAsync(user)).ToList();
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            authModel.IsAuthenticated = true;
            return authModel;
        }

        public async Task<string> AddRole(RoleModel roleModel)
        {
            var user = await _userManager.FindByIdAsync(roleModel.UserId);
            if (user is null)
                return $"User With Id {roleModel.UserId} is not Found";
            if (!await _roleManager.RoleExistsAsync(roleModel.RoleName))
                return $"The Role {roleModel.RoleName} is not available";
            if (await _userManager.IsInRoleAsync(user, roleModel.RoleName))
                return $"This User has Already {roleModel.RoleName} Role";
            var res = await _userManager.AddToRoleAsync(user, roleModel.RoleName);
            return !res.Succeeded ? "Failed To Add Role" : $"Role {roleModel.RoleName} Added to {user.UserName} SuccessFully";
        }
    }
}
