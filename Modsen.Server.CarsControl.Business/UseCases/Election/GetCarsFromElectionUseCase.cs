using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.UseCases.Election
{
    public class GetCarsFromElectionUseCase
    {
        private readonly IMongoRepository<BsonDocument> _mongoRepository;
        public GetCarsFromElectionUseCase(IMongoRepositoryFactory<BsonDocument> mongoRepositoryFactory)
        {
            _mongoRepository = mongoRepositoryFactory.Create("Election");
        }

        public async Task<string> GetCarsAsync()
        {
            var cars = await _mongoRepository.GetAllAsync();
        
            return cars.ToJson();
        }
    }
}
