using MediatR;

namespace Modsen.Server.CarsElections.Application.Features.Car.Commands
{
    public record DeleteCar : IRequest
    {
        public string Id { get; set; } = null!;
    }
}
