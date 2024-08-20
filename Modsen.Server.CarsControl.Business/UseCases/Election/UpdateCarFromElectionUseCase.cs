using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.UseCases.Election
{
    public class UpdateCarFromElectionUseCase
    {
        private readonly IMongoRepository<BsonDocument> _mongoRepository;
        public UpdateCarFromElectionUseCase(IMongoRepositoryFactory<BsonDocument> mongoRepositoryFactory)
        {
            _mongoRepository = mongoRepositoryFactory.Create("Election");
        }

        public async Task UpdateCarAsync(string id, BsonDocument car)
        {
            await _mongoRepository.UpdateAsync(id, car);
        }
    }
}
