using MediatR;

namespace Modsen.Server.CarsElections.Application.Features.Car.Commands
{
    public record AddCar : IRequest
    {
        public string Id { get; init; } = null!;
    }
}
