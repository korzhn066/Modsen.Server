using AutoMapper;
using MediatR;
using Modsen.Server.CarsElections.Application.Features.Car.Commands;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;

namespace Modsen.Server.CarsElections.Application.Features.Car.CommandsHandlers
{
    public class AddCarHandler(
        ICarRepository carRepository,
        IMapper mapper) : IRequestHandler<AddCar>
    {
        private readonly ICarRepository _carRepository = carRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(AddCar request, CancellationToken cancellationToken)
        {
            await _carRepository.AddAsync(_mapper.Map<Domain.Entities.Car>(request), cancellationToken);

            await _carRepository.SaveChangesAsync();
        }
    }
}
