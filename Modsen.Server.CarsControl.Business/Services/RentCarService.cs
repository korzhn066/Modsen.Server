using Modsen.Server.CarsControl.Business.Interfaces;
using Modsen.Server.CarsControl.Business.Services.Base;
using Modsen.Server.CarsControl.DataAccess.Enums;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.Services
{
    public class RentCarService(IMongoRepositoryFactory<BsonDocument> mongoRepositoryFactory)
        : CarServiceBase(mongoRepositoryFactory.Create(nameof(CarType.Rent))), IRentCarService
    {
    }
}
