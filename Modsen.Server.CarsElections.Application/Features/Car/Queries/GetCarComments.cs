using MediatR;

namespace Modsen.Server.CarsElections.Application.Features.Car.Queries
{
    public record GetCarComments : IRequest<Domain.Entities.Car>
    {
        public string CarId { get; set; } = null!;
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
