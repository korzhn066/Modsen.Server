using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsControl.Business.UseCases.Rent
{
    public class RemoveCarFromRentUseCase
    {
        private readonly IMongoRepository<BsonDocument> _mongoRepository;

        public RemoveCarFromRentUseCase(IMongoRepositoryFactory<BsonDocument> mongoRepositoryFactory)
        {
            _mongoRepository = mongoRepositoryFactory.Create("Rent");
        }

        public async Task RemoveCarAsync(string id)
        {
            await _mongoRepository.DeleteAsync(id);
        }
    }
}
