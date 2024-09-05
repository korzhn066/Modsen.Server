using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Services;
using Modsen.Server.CarsControl.DataAccess.Repository;
using Modsen.Server.CarsControl.DataAccess.Services;
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

            services.AddGrpcClient<Car.CarClient>(options => options.Address = new Uri(builder.Configuration["Grpc:Host"]!));

            services.AddScoped<IMongoRepositoryFactory, MongoRepositoryFactory>();

            services.AddScoped<IGrpcService, GrpcService>();

            return services;
        }
    }
}
