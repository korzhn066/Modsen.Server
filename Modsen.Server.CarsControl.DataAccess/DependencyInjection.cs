using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using Modsen.Server.CarsControl.DataAccess.Repository;
using MongoDB.Driver;

namespace Modsen.Server.CarsControl.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(
            this IServiceCollection services,
            IHostApplicationBuilder builder)
        {
            services.AddSingleton(
                new MongoClient(builder.Configuration["MongoDbSettings:ConnectionString"])
                .GetDatabase(builder.Configuration["MongoDbSettings:DataBaseName"]));

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScoped(typeof(IMongoRepositoryFactory<>), typeof(MongoRepositoryFactory<>));

            return services;
        }
    }
}
