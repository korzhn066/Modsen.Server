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
    public class GetCurrentWinningCarHandler(ICarRepository carRepository)
        : IRequestHandler<GetCurrentWinningCar, Domain.Entities.Car>
    {
        private readonly ICarRepository _carRepository = carRepository;

        public async Task<Domain.Entities.Car> Handle(GetCurrentWinningCar request, CancellationToken cancellationToken)
        {
            return await _carRepository.Query
                .GetQuery(new CarOrderByDescendingScoreSpecification())
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException(ErrorConstants.CarNotFoundError);
        }
    }
}
