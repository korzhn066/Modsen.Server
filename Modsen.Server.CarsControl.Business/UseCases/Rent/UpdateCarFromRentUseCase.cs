using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.UseCases.Rent
{
    public class UpdateCarFromRentUseCase
    {
        private readonly IMongoRepository<BsonDocument> _mongoRepository;
        public UpdateCarFromRentUseCase(IMongoRepositoryFactory<BsonDocument> mongoRepositoryFactory)
        {
            _mongoRepository = mongoRepositoryFactory.Create("Rent");
        }

        public async Task UpdateCarAsync(string id, BsonDocument car)
        {
            await _mongoRepository.UpdateAsync(id, car);
        }
    }
}
