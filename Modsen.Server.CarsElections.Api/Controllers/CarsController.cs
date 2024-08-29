using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.CarsElections.Api.Models.Responses;
using Modsen.Server.CarsElections.Application.Features.Car.Queries;

namespace Modsen.Server.CarsElections.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController(
        IMediator mediator,
        IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [Route("winning_car")]
        public async Task<IActionResult> GetWiningCar(CancellationToken cancellationToken)
        {
            var car = await _mediator.Send(new GetCurrentWinningCar(), cancellationToken);

            return Ok(car);
        }

        [HttpGet]
        [Route("car_comments")]
        public async Task<IActionResult> GetCarComments(string carId, int page, int count, CancellationToken cancellationToken)
        {
            var cars = await _mediator.Send(new GetCarComments
            {
                CarId = carId,
                Page = page,
                Count = count
            }, cancellationToken);

            return Ok(
                cars.Comments.Select(_mapper.Map<CommentResponse>)
            );
        }
    }
}
