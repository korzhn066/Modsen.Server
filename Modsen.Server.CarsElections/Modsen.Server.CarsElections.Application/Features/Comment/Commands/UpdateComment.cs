using MediatR;

namespace Modsen.Server.CarsElections.Application.Features.Comment.Commands
{
    public record UpdateComment : IRequest
    {
        public int CommentId {  get; set; }
        public string Message { get; set; } = null!;
    }
}
