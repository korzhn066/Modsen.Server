using Microsoft.Extensions.DependencyInjection;
using Modsen.Server.CarsElections.Application.Mapper;

namespace Modsen.Server.CarsElections.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddAutoMapper(typeof(CommentMappingProfile));

            return services;
        }
    };
}
