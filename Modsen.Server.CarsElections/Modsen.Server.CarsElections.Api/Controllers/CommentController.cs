using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.CarsElections.Api.Models.Requests;
using Modsen.Server.CarsElections.Api.Models.Responses;
using Modsen.Server.CarsElections.Application.Features.Comment.Commands;
using Modsen.Server.CarsElections.Application.Features.Comment.Queries;
using Modsen.Server.Shared.Helpers;

namespace Modsen.Server.CarsElections.Api.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController(
        IMediator mediator,
        IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentRequest commentRequest, CancellationToken cancellationToken)
        {
            var addComment = _mapper.Map<AddComment>(commentRequest);
            addComment.UserName = AuthenticateHelper.GetUserName(HttpContext);

            await _mediator.Send(addComment, cancellationToken);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteComment(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteComment
            {
                CommentId = id,
            }, cancellationToken);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(
            UpdateComment updateComment,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(updateComment, cancellationToken);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetComment(string carId, CancellationToken cancellationToken)
        {
            var comment = await _mediator.Send(new GetCommentByUserName
            {
                UserName = AuthenticateHelper.GetUserName(HttpContext),
                CarId = carId
            }, cancellationToken);

            return Ok(
                _mapper.Map<CommentResponse>(comment)
            );
        }
    }
}
