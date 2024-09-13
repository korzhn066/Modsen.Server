using Grpc.Core;
using MediatR;
using Modsen.Server.CarsElections.Application.Features.Car.Commands;

namespace Modsen.Server.CarsElections.Api.Services
{
    public class CarService(IMediator mediator) : Car.CarBase
    {
        private readonly IMediator _mediator = mediator;

        public override async Task<CarReply> AddCar(CarRequest request, ServerCallContext context)
        {
            await _mediator.Send(new AddCar { Id = request.Id });

            return new CarReply();
        }
    }
}
