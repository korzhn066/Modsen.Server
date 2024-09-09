using MediatR;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Application.Features.Car.Queries;
using Modsen.Server.CarsElections.Application.Specifications;
using Modsen.Server.CarsElections.Application.Specifications.CarSpecificaions;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;

namespace Modsen.Server.CarsElections.Application.Features.Car.QueryHandlers
{
    public class GetCarCommentsHandler(
        ICarRepository carRepository,
        ICacheRepository cacheRepository)
        : IRequestHandler<GetCarComments, Domain.Entities.Car>
    {
        private readonly ICarRepository _carRepository = carRepository;
        private readonly ICacheRepository _cacheRepository = cacheRepository;

        public async Task<Domain.Entities.Car> Handle(GetCarComments request, CancellationToken cancellationToken)
        {
            var carComments = await _cacheRepository.GetAsync<Domain.Entities.Car>("carComments");

            if (carComments is null)
            {
                carComments = await _carRepository.Query
                    .GetQuery(new IncludeCarCommentsSpecification(request.Page, request.Count))
                    .GetQuery(new CarIdSpecification(request.CarId))
                    .FirstOrDefaultAsync(cancellationToken);

                await _cacheRepository.SetAsync("carComments", carComments, 1000);
            }

            return carComments ?? throw new NotFoundException(ErrorConstants.CarNotFoundError);
        }
    }
}
