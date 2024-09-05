using Microsoft.Extensions.DependencyInjection;
using Modsen.Server.Authentication.Application.Mapper;

namespace Modsen.Server.Authentication.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserMappingProfile));

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            return services;
        }
    }
}
