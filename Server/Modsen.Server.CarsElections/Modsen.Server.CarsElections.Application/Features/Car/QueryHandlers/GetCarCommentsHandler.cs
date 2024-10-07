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
    public class GetCarCommentsHandler(
        ICarRepository carRepository,
        ILogger<GetCarComments> logger,
        ICacheRepository cacheRepository)
        : IRequestHandler<GetCarComments, Domain.Entities.Car>
    {
        private readonly ICarRepository _carRepository = carRepository;
        private readonly ICacheRepository _cacheRepository = cacheRepository;
        private readonly ILogger<GetCarComments> _logger = logger;

        public async Task<Domain.Entities.Car> Handle(GetCarComments request, CancellationToken cancellationToken)
        {
            var carComments = await _cacheRepository.GetAsync<Domain.Entities.Car>("carComments");

            if (carComments is not null)
            {
                _logger.LogInformation("Get comments");

                return carComments;
            }
            
            carComments = await _carRepository.Query
                .GetQuery(new IncludeCarCommentsSpecification(request.Page, request.Count))
                .GetQuery(new CarIdSpecification(request.CarId))
                .FirstOrDefaultAsync(cancellationToken);

            if (carComments is null)
            {
                _logger.LogError("Car is null when getting comments");

                throw new NotFoundException(ErrorConstants.CarNotFoundError);
            }
            
            await _cacheRepository.SetAsync("carComments", carComments, 1000);          

            _logger.LogInformation("Get comments");

            return carComments;
        }
    }
}
