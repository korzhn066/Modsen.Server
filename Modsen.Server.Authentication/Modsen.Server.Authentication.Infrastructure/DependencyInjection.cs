using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modsen.Server.Authentication.Domain.Entities;
using Modsen.Server.Authentication.Domain.Interfaces.Services;
using Modsen.Server.Authentication.Infrastructure.Data;
using Modsen.Server.Authentication.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using MassTransit;
using Modsen.Server.Shared.Models.Kafka;

namespace Modsen.Server.Authentication.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

            services.AddScoped<ITokenProviderService, TokenProviderService>();

            services.AddMassTransit(options =>
            {
                options.UsingInMemory();

                options.AddRider(rider =>
                {
                    rider.AddProducer<UserCreated>("User");

                    rider.UsingKafka((context, k) => k.Host(builder.Configuration["Kafka:BootstrapServers"]));
                });
            });

            return services;
        }
    }
}
