using Modsen.Server.CarsControl.Business.Interfaces;
using Modsen.Server.CarsControl.Business.Services.Base;
using Modsen.Server.CarsControl.DataAccess.Enums;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Services;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.Services
{
    public class RentCarService(IMongoRepositoryFactory mongoRepositoryFactory, IGrpcService grpcService)
        : CarServiceBase(mongoRepositoryFactory.Create(nameof(CarType.Rent)), grpcService), IRentCarService
    {
    }
}
