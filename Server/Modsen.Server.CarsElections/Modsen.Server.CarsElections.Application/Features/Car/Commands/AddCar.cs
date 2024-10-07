using MediatR;
using Modsen.Server.Shared.Enums;

namespace Modsen.Server.CarsElections.Application.Features.Car.Commands
{
    public record AddCar : IRequest
    {
        public string Id { get; init; } = null!;
        public CarType CarType { get; init; }
    }
}
