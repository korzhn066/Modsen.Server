using Microsoft.Extensions.DependencyInjection;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Queries;

namespace Modsen.Server.Authentication.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddTransient<AddApplicationUserToRoleByIdUseCase>();
            services.AddTransient<ChangeApplicationUserRefreshTokenUseCase>();
            services.AddTransient<ChangeApplicationUserStatusHandlerUseCase>();
            services.AddTransient<DenyApplicationUserRoleByIdUseCase>();
            services.AddTransient<RegisterUserUseCase>();
            services.AddTransient<LoginUserUseCase>();
            services.AddTransient<RefreshTokenUseCase>();

            services.AddTransient<GetApplicationUserByUsernameUseCases>();
            services.AddTransient<GetApplicationUsersUseCase>();

            return services;
        }
    }
}
