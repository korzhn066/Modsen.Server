using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.UseCases.Processing
{
    public class AddCarToProcessingUseCase
    {
        private readonly IMongoRepository<BsonDocument> _mongoRepository;
        public AddCarToProcessingUseCase(IMongoRepositoryFactory<BsonDocument> mongoRepositoryFactory)
        {
            _mongoRepository = mongoRepositoryFactory.Create("Processing");
        }

        public async Task AddCarAsync(BsonDocument car)
        {
            await _mongoRepository.AddAsync(car);
        }
    }
}
