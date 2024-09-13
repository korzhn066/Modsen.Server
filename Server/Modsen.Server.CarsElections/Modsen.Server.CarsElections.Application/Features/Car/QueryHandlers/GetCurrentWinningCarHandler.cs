using MediatR;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Application.Features.Car.Queries;
using Modsen.Server.CarsElections.Application.Specifications;
using Modsen.Server.CarsElections.Application.Specifications.CarSpecificaions;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Modsen.Server.CarsElections.Application.Features.Car.QueryHandlers
{
    public class GetCurrentWinningCarHandler(
        ICarRepository carRepository,
        ICacheRepository cacheRepository,
        ILogger<GetCurrentWinningCarHandler> logger)
        : IRequestHandler<GetCurrentWinningCar, Domain.Entities.Car>
    {
        private readonly ICarRepository _carRepository = carRepository;
        private readonly ICacheRepository _cacheRepository = cacheRepository;
        private readonly ILogger<GetCurrentWinningCarHandler> _logger = logger;

        public async Task<Domain.Entities.Car> Handle(GetCurrentWinningCar request, CancellationToken cancellationToken)
        {
            var currentWinningCar = await _cacheRepository.GetAsync<Domain.Entities.Car>("currentWinningCar");
            
            if (currentWinningCar is not null)
            {
                _logger.LogInformation("Get winning car");
            
                return currentWinningCar;
            }
        
            currentWinningCar = await _carRepository.Query
                .GetQuery(new CarOrderByDescendingScoreSpecification())
                .FirstOrDefaultAsync(cancellationToken);

            if (currentWinningCar is null)
            {
                _logger.LogError("Winning car not found");

                throw new NotFoundException(ErrorConstants.CarNotFoundError);
            }

            await _cacheRepository.SetAsync("currentWinningCar", currentWinningCar, 1000);

            _logger.LogInformation("Get winning car");

            return currentWinningCar;
        }
    }
}
