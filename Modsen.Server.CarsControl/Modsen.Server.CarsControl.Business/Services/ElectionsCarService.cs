using Microsoft.Extensions.Logging;
using Modsen.Server.CarsControl.Business.Interfaces;
using Modsen.Server.CarsControl.Business.Services.Base;
using Modsen.Server.CarsControl.DataAccess.Enums;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Services;

namespace Modsen.Server.CarsControl.Business.Services
{
    internal class ElectionsCarService(
        IMongoRepositoryFactory mongoRepositoryFactory,
        IGrpcService grpcService,
        ILogger<CarServiceBase> logger) : CarServiceBase(
            mongoRepositoryFactory.Create(nameof(CarType.Elections)), grpcService, logger), IElectionsCarService
    {
    }
}
