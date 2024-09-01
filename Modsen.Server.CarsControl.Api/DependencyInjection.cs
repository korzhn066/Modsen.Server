using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Driver;

namespace Modsen.Server.CarsControl.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresintation(
            this IServiceCollection services)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
