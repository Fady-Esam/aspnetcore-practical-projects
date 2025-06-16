using BL.Interfaces;
using FruitsAppBackEnd.BL;
using FruitsAppBackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BL.Classes
{
    public class AuthSerivce : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JWT _jwt;
        public AuthSerivce(UserManager<AppUser> userManager, JWT jwt)
        {
            _userManager = userManager;
            _jwt = jwt;
        }
        public async Task<AuthModel> RegisterWithEmailAndPassword(UserModel UserModel)
        {
            if (await _userManager.FindByNameAsync(UserModel.UserName) is not null)
            {
                return new AuthModel
                {
                    Message = "UserName already exists",
                    StatusCode = "400"
                };
            }
            if (await _userManager.FindByEmailAsync(UserModel.Email) is not null)
            {
                return new AuthModel
                {
                    Message = "Email already exists",
                    StatusCode = "400"
                };
            }
            var user = new AppUser
            { 
                UserName = UserModel.UserName,
                Email = UserModel.Email,
            };
            await _userManager.AddToRoleAsync(user, "User");
            var res =  await _userManager.CreateAsync(user, UserModel.Password);
            if (!res.Succeeded)
            {
                return new AuthModel
                {
                    Message = "Errors Found",
                    Errors = res.Errors.Select(i => i.Description).ToList(),
                    StatusCode = "400",
                };
            }
            var token = await GetToken(user);
            return new AuthModel
            {
                Message = "User Created Successfully",
                isAuthenticated = true,
                StatusCode = "201",
                Roles = new List<string> { "User" },
                ExpiresOn = token.ValidTo,
                UserModel = new UserModel { UserName = user.UserName, Email = user.Email },
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };
        }
        public async Task<AuthModel> LoginWithEmailAndPassword(UserModel UserModel)
        {
            var user = await _userManager.FindByEmailAsync(UserModel.Email);
            if(user is null || !await _userManager.CheckPasswordAsync(user, UserModel.Password))
            {
                return new AuthModel
                {
                    Message = "Invalid Email Or Password",
                    StatusCode = "400"
                };
            }
            var token = await GetToken(user);
            return new AuthModel
            {
                Message = "Logged In Successfully",
                isAuthenticated = true,
                StatusCode = "200",
                Roles = (await _userManager.GetRolesAsync(user)).ToList(),
                ExpiresOn = token.ValidTo,
                UserModel = new UserModel { UserName = user.UserName!, Email = user.Email! },
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };
        }
        public async Task<JwtSecurityToken> GetToken(AppUser appUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, appUser?.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Email, appUser?.Email ?? ""),
            };
            var roles = await _userManager.GetRolesAsync(appUser);
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var credintial = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMonths(_jwt.DurationInMonths),
                    signingCredentials: credintial
                );
            return jwtToken;
        }

        public async Task<AuthModel> ChangePassword(UserModel UserModel, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(UserModel.Email);
            if(user is null)
            {
                return new AuthModel
                {
                    Message = "Invalid Email",
                    StatusCode = "400"
                };
            }
            var res = await _userManager.ChangePasswordAsync(user, UserModel.Password, newPassword);
            if (!res.Succeeded)
            {
                return new AuthModel
                {
                    Message = "Errors Found",
                    Errors = res.Errors.Select(i => i.Description).ToList(),
                    StatusCode = "400",
                };
            }
            var token = await GetToken(user);
            return new AuthModel
            {
                Message = "Password Changed Successfully",
                isAuthenticated = true,
                StatusCode = "200",
                Roles = (await _userManager.GetRolesAsync(user)).ToList(),
                ExpiresOn = token.ValidTo,
                UserModel = new UserModel { UserName = user.UserName!, Email = user.Email! },
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };

        }
    }
}
