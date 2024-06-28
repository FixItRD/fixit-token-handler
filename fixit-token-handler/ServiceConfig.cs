using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace fixit_token_handler
{
    public static class ServiceConfig
    {
        public static void ConfigureCORS(this IServiceCollection services, string MyAllowSpecifiOrigins)
        {
            string[] domains = { "", "https://localhost:32776", "http://localhost:32777", "https://localhost:32778", "http://localhost:32779" };


            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecifiOrigins,
                    policy =>
                    {
                        policy.WithOrigins(domains)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = config["JwtSettings:Issuer"],
                        ValidAudience = config["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });
        }
    }
}
