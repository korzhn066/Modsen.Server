using Microsoft.AspNetCore.Http.HttpResults;
using Modsen.Server.CarsControl.Business.Interfaces.Base;
using Modsen.Server.CarsControl.DataAccess.Entities;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Services;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.Services.Base
{
    public abstract class CarServiceBase(
        IMongoRepository mongoRepository,
        IGrpcService grpcService) : ICarServiceBase
    {
        protected readonly IMongoRepository MongoRepository = mongoRepository;
        private readonly IGrpcService _grpcService = grpcService;

        public async Task AddCarAsync(string car)
        {
            var id = await MongoRepository.AddAsync(new CarDocument { Content = BsonDocument.Parse(car) });

            await _grpcService.AddCarAsync(id);
        }

        public async Task DeleteCarAsync(string carId)
        {
            var result = await MongoRepository.DeleteAsync(carId);

            if (result.DeletedCount < 1)
            {
                throw new NotFoundException(ErrorConstants.NotFoundEntityError);
            }
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
            var result = await MongoRepository.UpdateAsync(carId, BsonDocument.Parse(car));

            if (!result.IsAcknowledged)
            {
                throw new NotFoundException(ErrorConstants.NotFoundEntityError);
            }
        }
    }
}
