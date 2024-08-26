using Microsoft.Extensions.DependencyInjection;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using Modsen.Server.CarsControl.DataAccess.Repository;
using MongoDB.Driver;

namespace Modsen.Server.CarsControl.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(
            this IServiceCollection services)
        {
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScoped(typeof(IMongoRepositoryFactory<>), typeof(MongoRepositoryFactory<>));

            return services;
        }
    }
}
