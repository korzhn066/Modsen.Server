using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.CarsElections.Api.Models.Requests;
using Modsen.Server.CarsElections.Application.Features.Like.Command;
using Modsen.Server.CarsElections.Domain.Enums;

namespace Modsen.Server.CarsElections.Api.Controllers
{
    [Route("api/likes/")]
    [ApiController]
    public class LikeController(
        IMediator mediator,
        IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> PutLike(LikeRequest likeRequest, CancellationToken cancellationToken)
        {
            var putLike = _mapper.Map<PutLike>(likeRequest);
            putLike.UserName = "Test";

            await _mediator.Send(putLike, cancellationToken);

            return Ok();
        }
    }
}
