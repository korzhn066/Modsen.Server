using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modsen.Server.CarsElections.Application.Mapper;
using Modsen.Server.CarsElections.Application.Services;
using Modsen.Server.Shared.Models.Kafka;

namespace Modsen.Server.CarsElections.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddAutoMapper(typeof(CommentMappingProfile), typeof(UserMappingProfile));

            services.AddMediator(configuration => configuration.AddConsumer<UserCreatedConsumer>());

            services.AddMassTransit(options =>
            {
                options.UsingInMemory();

                options.AddRider(rider =>
                {
                    rider.AddConsumer<UserCreatedConsumer>();

                    rider.UsingKafka((context, k) =>
                    {
                        k.Host(configuration["Kafka:BootstrapServers"]);

                        k.TopicEndpoint<UserCreated>("User", configuration["Kafka:GroupId"], e =>
                        {
                            e.ConfigureConsumer<UserCreatedConsumer>(context);
                        });
                    });
                });
            });

            return services;
        }
    };
}
