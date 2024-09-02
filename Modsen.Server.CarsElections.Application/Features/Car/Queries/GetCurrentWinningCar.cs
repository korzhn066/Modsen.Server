using MediatR;

namespace Modsen.Server.CarsElections.Application.Features.Car.Queries
{
    public record GetCurrentWinningCar : IRequest<Domain.Entities.Car>
    {
    }
}
