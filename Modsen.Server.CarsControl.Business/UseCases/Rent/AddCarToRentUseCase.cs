using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.UseCases.Rent
{
    public class AddCarToRentUseCase
    {
        private readonly IMongoRepository<BsonDocument> _mongoRepository;
        public AddCarToRentUseCase(IMongoRepositoryFactory<BsonDocument> mongoRepositoryFactory)
        {
            _mongoRepository = mongoRepositoryFactory.Create("Rent");
        }

        public async Task AddCarAsync(BsonDocument car)
        {
            await _mongoRepository.AddAsync(car);
        }
    }
}
