using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.UseCases.Processing
{
    public class UpdateCarFromProcessingUseCase
    {
        private readonly IMongoRepository<BsonDocument> _mongoRepository;
        public UpdateCarFromProcessingUseCase(IMongoRepositoryFactory<BsonDocument> mongoRepositoryFactory)
        {
            _mongoRepository = mongoRepositoryFactory.Create("Processing");
        }

        public async Task UpdateCarAsync(string id, BsonDocument car)
        {
            await _mongoRepository.UpdateAsync(id, car);
        }
    }
}
