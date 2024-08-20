using Microsoft.Extensions.DependencyInjection;
using Modsen.Server.CarsControl.Business.UseCases.Rent;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsControl.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddTransient<AddCarToRentUseCase>();
            services.AddTransient<GetCarsFromRentUseCase>();

            return services;
        }
    }
}
