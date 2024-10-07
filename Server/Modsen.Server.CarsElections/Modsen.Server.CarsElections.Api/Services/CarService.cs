using AutoMapper;
using Grpc.Core;
using MediatR;
using Modsen.Server.CarsElections.Application.Features.Car.Commands;

namespace Modsen.Server.CarsElections.Api.Services
{
    public class CarService(
        IMediator mediator,
        IMapper mapper) : Car.CarBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        public override async Task<CarReply> AddCar(CarRequest request, ServerCallContext context)
        {
            await _mediator.Send(_mapper.Map<AddCar>(request));

            return new CarReply();
        }

        public override async Task<CarReply> UpdateCar(UpdateCarRequest request, ServerCallContext context)
        {
            await _mediator.Send(_mapper.Map<UpdateCar>(request));

            return new CarReply();
        }

        public override async Task<CarReply> DeleteCar(DeleteCarRequest request, ServerCallContext context)
        {
            await _mediator.Send(_mapper.Map<DeleteCar>(request));

            return new CarReply();
        }
    }
}
