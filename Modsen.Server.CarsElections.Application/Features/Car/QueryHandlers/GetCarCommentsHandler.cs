using MediatR;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Application.Features.Car.Queries;
using Modsen.Server.CarsElections.Application.Specifications;
using Modsen.Server.CarsElections.Application.Specifications.CarSpecificaions;
using Modsen.Server.CarsElections.Domain.Constants;
using Modsen.Server.CarsElections.Domain.Exceptions;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;

namespace Modsen.Server.CarsElections.Application.Features.Car.QueryHandlers
{
    public class GetCarCommentsHandler(ICarRepository carRepository)
        : IRequestHandler<GetCarComments, Domain.Entities.Car>
    {
        private readonly ICarRepository _carRepository = carRepository;

        public async Task<Domain.Entities.Car> Handle(GetCarComments request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.Query
                .GetQuery(new IncludeCarCommentsSpecification(request.Page, request.Count))
                .GetQuery(new CarIdSpecification(request.CarId))
                .FirstOrDefaultAsync(cancellationToken);

            return car ?? throw new NotFoundException(ErrorConstants.CarNotFoundError);
        }
    }
}
