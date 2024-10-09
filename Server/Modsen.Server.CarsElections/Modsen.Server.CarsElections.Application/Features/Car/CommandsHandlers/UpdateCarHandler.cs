using MediatR;
using Microsoft.Extensions.Logging;
using Modsen.Server.CarsElections.Application.Features.Car.Commands;
using Modsen.Server.CarsElections.Application.Specifications.CarSpecificaions;
using Modsen.Server.CarsElections.Application.Specifications;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Modsen.Server.CarsElections.Application.Features.Car.CommandsHandlers
{
    public class UpdateCarHandler(ICarRepository carRepository, ILogger<UpdateCarHandler> logger) : IRequestHandler<UpdateCar>
    {
        private readonly ICarRepository _carRepository = carRepository;
        private readonly ILogger<UpdateCarHandler> _logger = logger;

        public async Task Handle(UpdateCar request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.Query
                .GetQuery(new CarIdSpecification(request.Id))
                .FirstOrDefaultAsync();

            if (car is null)
            {
                _logger.LogError("Car not found");

                throw new NotFoundException(ErrorConstants.CarNotFoundError);
            }

            car.CarType = request.CarType;

            await _carRepository.SaveChangesAsync();

            _logger.LogInformation("Car changed");
        }
    }
}
