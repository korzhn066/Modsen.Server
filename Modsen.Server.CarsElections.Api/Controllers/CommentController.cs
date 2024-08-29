using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.CarsElections.Api.Models.Requests;
using Modsen.Server.CarsElections.Api.Models.Responses;
using Modsen.Server.CarsElections.Application.Features.Comment.Commands;
using Modsen.Server.CarsElections.Application.Features.Comment.Queries;

namespace Modsen.Server.CarsElections.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(
        IMediator mediator,
        IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        [Route("add_comment")]
        public async Task<IActionResult> AddComment(CommentRequest commentRequest, CancellationToken cancellationToken)
        {
            var addComment = _mapper.Map<AddComment>(commentRequest);
            addComment.UserName = "Test1";

            await _mediator.Send(addComment, cancellationToken);

            return NoContent();
        }

        [HttpDelete]
        [Route("delete_comment")]
        public async Task<IActionResult> DeleteComment(int commentId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteComment
            {
                CommentId = commentId,
            }, cancellationToken);

            return NoContent();
        }

        [HttpPost]
        [Route("update_comment")]
        public async Task<IActionResult> UpdateComment(
            UpdateComment updateComment,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(updateComment, cancellationToken);

            return NoContent();
        }

        [HttpGet]
        [Route("comment")]
        public async Task<IActionResult> GetComment(string carId, CancellationToken cancellationToken)
        {
            var comment = await _mediator.Send(new GetCommentByUserName
            {
                UserName = "Test",
                CarId = carId
            }, cancellationToken);

            return Ok(
                _mapper.Map<CommentResponse>(comment)
            );
        }
    }
}
