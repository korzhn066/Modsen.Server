using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.UseCases.Election
{
    public class AddCarToElectionUseCase
    {
        private readonly IMongoRepository<BsonDocument> _mongoRepository;
        public AddCarToElectionUseCase(IMongoRepositoryFactory<BsonDocument> mongoRepositoryFactory)
        {
            _mongoRepository = mongoRepositoryFactory.Create("Election");
        }

        public async Task AddCarAsync(BsonDocument car)
        {
            await _mongoRepository.AddAsync(car);
        }
    }
}
