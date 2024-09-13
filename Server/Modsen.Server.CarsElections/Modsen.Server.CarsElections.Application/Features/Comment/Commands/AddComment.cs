using MediatR;
using Modsen.Server.CarsElections.Domain.Enums;

namespace Modsen.Server.CarsElections.Application.Features.Comment.Commands
{
    public record AddComment : IRequest
    {
        public string UserName { get; set; } = null!;
        public string Message { get; set; } = null!;
        public CommentType CommentType { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CarId { get; set; } = null!;
    }
}
