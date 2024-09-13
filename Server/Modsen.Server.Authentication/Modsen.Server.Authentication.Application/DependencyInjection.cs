using Microsoft.Extensions.DependencyInjection;
using Modsen.Server.Authentication.Application.Mapper;
using FluentValidation;
using Modsen.Server.Authentication.Application.Validators.Authentication;
using Modsen.Server.Authentication.Application.Pipelines;

namespace Modsen.Server.Authentication.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<RegisterApplicationUserValidator>();

            services.AddAutoMapper(typeof(UserMappingProfile));

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            return services;
        }
    }
}
