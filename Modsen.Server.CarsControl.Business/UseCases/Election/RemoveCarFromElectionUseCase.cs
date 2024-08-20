using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsControl.Business.UseCases.Election
{
    public class RemoveCarFromElectionUseCase
    {
        private readonly IMongoRepository<BsonDocument> _mongoRepository;
        public RemoveCarFromElectionUseCase(IMongoRepositoryFactory<BsonDocument> mongoRepositoryFactory)
        {
            _mongoRepository = mongoRepositoryFactory.Create("Election");
        }

        public async Task RemoveCarAsync(string id)
        {
            await _mongoRepository.DeleteAsync(id);
        }
    }
}
