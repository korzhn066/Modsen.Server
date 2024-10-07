using MediatR;
using Modsen.Server.Shared.Enums;

namespace Modsen.Server.CarsElections.Application.Features.Car.Commands
{
    public record UpdateCar : IRequest
    {
        public string Id { get; set; } = null!;
        public CarType CarType { get; set; }
    }
}
