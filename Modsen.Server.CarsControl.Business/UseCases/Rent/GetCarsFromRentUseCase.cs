using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsControl.Business.UseCases.Rent
{
    public class GetCarsFromRentUseCase
    {
        private readonly IMongoRepository<BsonDocument> _mongoRepository;

        public GetCarsFromRentUseCase(IMongoRepositoryFactory<BsonDocument> mongoRepositoryFactory)
        {
            _mongoRepository = mongoRepositoryFactory.Create("Rent");
        }

        public async Task<string> GetCarsAsync()
        {
            var cars = await _mongoRepository.GetAllAsync();

            return cars.ToJson();
        }
    }
}
