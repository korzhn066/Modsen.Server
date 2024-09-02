using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Modsen.Server.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddShared(
            this IServiceCollection services)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = "http://localhost:4200",
                        ValidIssuer = "http://localhost:5000",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTRefreshTokenHIGHsecuredPasswordVVVp1OH7Xzyr")),
                    };
                });


            return services;
        }
    }
}
