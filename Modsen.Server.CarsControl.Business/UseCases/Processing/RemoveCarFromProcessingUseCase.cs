using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.UseCases.Processing
{
    public class RemoveCarFromProcessingUseCase
    {
        private readonly IMongoRepository<BsonDocument> _mongoRepository;
        public RemoveCarFromProcessingUseCase(IMongoRepositoryFactory<BsonDocument> mongoRepositoryFactory)
        {
            _mongoRepository = mongoRepositoryFactory.Create("Processing");
        }

        public async Task RemoveCarAsync(string id)
        {
            await _mongoRepository.DeleteAsync(id);
        }
    }
}
