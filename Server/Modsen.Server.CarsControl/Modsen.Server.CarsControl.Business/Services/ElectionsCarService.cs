using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Modsen.Server.CarsControl.Business.Interfaces;
using Modsen.Server.CarsControl.Business.Services.Base;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Services;
using Modsen.Server.Shared.Exceptions;
using Modsen.Server.CarsControl.Business.Models.Requests;
using Modsen.Server.CarsControl.DataAccess.Entities;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Business.Services
{
    public class ElectionsCarService(
        IMongoRepositoryFactory mongoRepositoryFactory,
        IGrpcService grpcService,
        ILogger<CarServiceBase> logger,
        ILogger<ElectionsCarService> currentLogger,
        IWebHostEnvironment webHostEnvironment) : CarServiceBase(
            mongoRepositoryFactory.Create(nameof(CarType.Elections)), 
            grpcService,
            logger,
            webHostEnvironment), IElectionsCarService
    {
        private readonly IMongoRepositoryFactory _mongoRepositoryFactory = mongoRepositoryFactory;
        private readonly ILogger<ElectionsCarService> _logger = currentLogger;
        private readonly IGrpcService _grpcService = grpcService;

        public async Task MoveAsync(MoveCar moveCar, CancellationToken cancellationToken = default)
        {
            var car = await MongoRepository.GetByIdAsync(moveCar.Id, cancellationToken);

            if (car is null) 
            {
                _logger.LogError("Car not found");

                throw new NotFoundException(nameof(car));
            }

            await _mongoRepositoryFactory.Create(moveCar.CarType.ToString()).AddAsync(car);

            await MongoRepository.DeleteAsync(moveCar.Id);

            await _grpcService.UpdateCarAsync(new UpdateCarRequest
            {
                Id = car.Id.ToString(),
                CarType = moveCar.CarType,
            }, cancellationToken);
        }

        public async Task AddCarAsync(AddCar addCar)
        {
            var photos = new BsonArray();

            foreach (var file in addCar.FormFiles)
            {
                var fileName = UploadFile(file);

                photos.Add(fileName);
            }

            var document = new CarDocument
            {
                Name = addCar.Name,
                Description = addCar.Description,
                Content = BsonDocument.Parse(addCar.Json),
                Photos = photos
            };

            await MongoRepository.AddAsync(document);

            _logger.LogInformation("Add car");

            await _grpcService.AddCarAsync(new CarRequest
            {
                Id = document.Id.ToString(),
                CarType = CarType.Elections
            });
        }
    }
}
