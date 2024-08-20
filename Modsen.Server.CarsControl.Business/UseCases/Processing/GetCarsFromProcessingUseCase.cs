using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.UseCases.Processing
{
    public class GetCarsFromProcessingUseCase
    {
        private readonly IMongoRepository<BsonDocument> _mongoRepository;
        public GetCarsFromProcessingUseCase(IMongoRepositoryFactory<BsonDocument> mongoRepositoryFactory)
        {
            _mongoRepository = mongoRepositoryFactory.Create("Processing");
        }

        public async Task<string> GetCarsAsync()
        {
            var cars = await _mongoRepository.GetAllAsync();
        
            return cars.ToJson();
        }
    }
}
