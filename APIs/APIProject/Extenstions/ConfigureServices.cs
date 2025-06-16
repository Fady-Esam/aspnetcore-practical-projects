using APIProject.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace APIProject.Extenstions
{
    public class ConfigureServicesCls
    {
        private readonly IConfiguration _configuration;
        public ConfigureServicesCls(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // JWT Authentication
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
            services.AddAuthentication(
                (r) =>
                {
                    r.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    r.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    r.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }) 
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _configuration["JwtSettings:Issuer"],
                        ValidAudience = _configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });
        }

    }
}
