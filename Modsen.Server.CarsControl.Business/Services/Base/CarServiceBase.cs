using Modsen.Server.CarsControl.Business.Interfaces.Base;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.Services.Base
{
    public abstract class CarServiceBase(IMongoRepository<BsonDocument> mongoRepository) : ICarServiceBase
    {
        protected readonly IMongoRepository<BsonDocument> MongoRepository = mongoRepository;

        public async Task AddCarAsync(string car)
        {
            await MongoRepository.AddAsync(BsonDocument.Parse(car));
        }

        public async Task DeleteCarAsync(string carId)
        {
            await MongoRepository.DeleteAsync(carId);
        }

        public async Task<string> GetCarAsync(string carId, CancellationToken cancellationTokend)
        {
            var car = await MongoRepository.GetByIdAsync(carId, cancellationTokend);
        
            return car.ToJson();
        }

        public async Task<string> GetCarsAsync(int page, int count, CancellationToken cancellationToken)
        {
            var cars = await MongoRepository
                .GetAllAsync(page, count, cancellationToken);

            return cars.ToJson();
        }

        public async Task UpdateCarAsync(string carId, string car)
        {
            await MongoRepository.UpdateAsync(carId, BsonDocument.Parse(car));
        }
    }
}
