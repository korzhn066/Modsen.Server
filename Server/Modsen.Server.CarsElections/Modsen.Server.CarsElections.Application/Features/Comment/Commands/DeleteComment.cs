using MediatR;

namespace Modsen.Server.CarsElections.Application.Features.Comment.Commands
{
    public record DeleteComment : IRequest
    {
        public int CommentId { get; set; }
    }
}
