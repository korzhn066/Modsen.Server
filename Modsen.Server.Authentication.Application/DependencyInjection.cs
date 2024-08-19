using Microsoft.Extensions.DependencyInjection;
using Modsen.Server.Authentication.Application.UseCases.Authentication;

namespace Modsen.Server.Authentication.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddTransient<RegisterUserUseCase>();
            services.AddTransient<LoginUserUseCase>();
            services.AddTransient<RevokeTokenUseCase>();
            services.AddTransient<RefreshTokenUseCase>();

            return services;
        }
    }
}
