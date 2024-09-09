using MediatR;

namespace Modsen.Server.CarsElections.Application.Features.Comment.Commands
{
    public record UpdateComment : IRequest<Domain.Entities.Comment>
    {
        public int CommentId {  get; set; }
        public string Message { get; set; } = null!;
    }
}
