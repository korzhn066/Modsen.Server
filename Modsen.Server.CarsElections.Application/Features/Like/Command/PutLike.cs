using MediatR;
using Modsen.Server.CarsElections.Domain.Enums;

namespace Modsen.Server.CarsElections.Application.Features.Like.Command
{
    public record PutLike : IRequest
    {
        public LikeType LikeType { get; set; }
        public int CommentId { get; set; }
        public string UserName { get; set; } = null!;
    }
}
