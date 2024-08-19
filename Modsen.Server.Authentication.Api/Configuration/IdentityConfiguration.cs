using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Domain.Entities;
using Modsen.Server.Authentication.Infrastructure.Data;

namespace Modsen.Server.Authentication.Api.Configuration
{
    internal static class IdentityConfiguration
    {
        internal static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<DBContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
