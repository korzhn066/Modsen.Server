using Microsoft.Extensions.DependencyInjection;
using Modsen.Server.CarsControl.Business.Interfaces;
using Modsen.Server.CarsControl.Business.Services;

namespace Modsen.Server.CarsControl.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<IRentCarService, RentCarService>();
            services.AddScoped<IProcessingCarService, ProcessingCarService>();
            services.AddScoped<IElectionsCarService, ElectionsCarService>();

            return services;
        }
    }
}
