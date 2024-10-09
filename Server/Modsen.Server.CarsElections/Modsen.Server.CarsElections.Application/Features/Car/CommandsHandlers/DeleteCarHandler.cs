using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modsen.Server.CarsElections.Application.Features.Car.Commands;
using Modsen.Server.CarsElections.Application.Specifications;
using Modsen.Server.CarsElections.Application.Specifications.CarSpecificaions;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;

namespace Modsen.Server.CarsElections.Application.Features.Car.CommandsHandlers
{
    public class DeleteCarHandler(ICarRepository carRepository, ILogger<DeleteCarHandler> logger) : IRequestHandler<DeleteCar>
    {
        private readonly ICarRepository _carRepository = carRepository;
        private readonly ILogger<DeleteCarHandler> _logger = logger;

        public async Task Handle(DeleteCar request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.Query
                .GetQuery(new CarIdSpecification(request.Id))
                .FirstOrDefaultAsync();

            if (car is null)
            {
                _logger.LogError("Car not found");

                throw new NotFoundException(ErrorConstants.CarNotFoundError);
            }

            _carRepository.Delete(car);

            await _carRepository.SaveChangesAsync();

            _logger.LogInformation("Car deleted");
        }
    }
}
