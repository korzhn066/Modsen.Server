using Microsoft.AspNetCore.Hosting;
using Modsen.Server.CarsControl.Business.Interfaces;
using Modsen.Server.CarsControl.Business.Services.Base;
using Modsen.Server.CarsControl.DataAccess.Enums;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Services;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.Services
{
    internal class ElectionsCarService(
        IMongoRepositoryFactory mongoRepositoryFactory,
        IGrpcService grpcService,
        IWebHostEnvironment webHostEnvironment) : CarServiceBase(
            mongoRepositoryFactory.Create(nameof(CarType.Elections)), 
            grpcService,
            webHostEnvironment), IElectionsCarService
    {
    }
}
