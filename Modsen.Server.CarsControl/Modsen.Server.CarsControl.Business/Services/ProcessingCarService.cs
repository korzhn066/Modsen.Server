using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Modsen.Server.CarsControl.Business.Interfaces;
using Modsen.Server.CarsControl.Business.Services.Base;
using Modsen.Server.CarsControl.DataAccess.Enums;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Services;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.Services
{
    internal class ProcessingCarService(
        IMongoRepositoryFactory mongoRepositoryFactory,
        IGrpcService grpcService,
        IWebHostEnvironment webHostEnvironment
        ILogger<CarServiceBase> logger) : CarServiceBase(
            mongoRepositoryFactory.Create(nameof(CarType.Processing)), 
            grpcService,
            webHostEnvironment,
            logger), IProcessingCarService
    {
    }
}
