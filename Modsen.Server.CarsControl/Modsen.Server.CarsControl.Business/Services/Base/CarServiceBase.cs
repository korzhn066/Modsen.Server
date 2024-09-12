using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        IGrpcService grpcService,
        ILogger<CarServiceBase> logger,
        IWebHostEnvironment webHostEnvironment) : ICarServiceBase
    {
        protected readonly IMongoRepository MongoRepository = mongoRepository;
        private readonly IGrpcService _grpcService = grpcService;
        private readonly ILogger<CarServiceBase> _logger = logger;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task DeleteCarAsync(string carId)
        {
            var result = await MongoRepository.DeleteAsync(carId);

            if (result.DeletedCount < 1)
            {
                _logger.LogError("Error when deleting car");

                throw new NotFoundException(ErrorConstants.NotFoundEntityError);
            }

            _logger.LogInformation("Car deleted");
        }

        public async Task<string> GetCarAsync(string carId, CancellationToken cancellationTokend)
        {
            var car = await MongoRepository.GetByIdAsync(carId, cancellationTokend);

            _logger.LogInformation("Get car");
            
            return car.ToJson();
        }

        public async Task<string> GetCarsAsync(int page, int count, CancellationToken cancellationToken)
        {
            var cars = await MongoRepository
                .GetAllAsync(page, count, cancellationToken);

            _logger.LogInformation("Get cars");

            return cars.ToJson();
        }

        public async Task UpdateCarAsync(string carId, string car)
        {
            var result = await MongoRepository.UpdateAsync(carId, BsonDocument.Parse(car));

            if (!result.IsAcknowledged)
            {
                _logger.LogError("Error when updating car");

                throw new NotFoundException(ErrorConstants.NotFoundEntityError);
            }

            _logger.LogInformation("Update car");
        }

        public async Task AddCarAsync(string car, IFormFileCollection formFiles)
        {
            var photos = new BsonArray();

            foreach (var file in formFiles)
            {
                var fileName = UploadFile(file);

                photos.Add(fileName);
            }

            var id = await MongoRepository.AddAsync(new CarDocument
            {
                Content = BsonDocument.Parse(car),
                Photos = photos
            });
            
            _logger.LogInformation("Add car");
            
            await _grpcService.AddCarAsync(id);
        }

        private string UploadFile(IFormFile file)
        {
            var rootPath = _webHostEnvironment.WebRootPath + "\\Images\\";
            var fileName = Guid.NewGuid().ToString() + file.FileName[file.FileName.LastIndexOf('.')..];

            if (file.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(rootPath))
                    {
                        Directory.CreateDirectory(rootPath);
                    }

                    using FileStream fileStream = File.Create(rootPath + fileName);
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                    return fileName;
                }
                catch
                {

                    throw new FileLoadException(ErrorConstants.FileLoadError);
                }
            }
            else
            {
                throw new FileLoadException(ErrorConstants.FileLoadError);
            }
        }
    }
}
