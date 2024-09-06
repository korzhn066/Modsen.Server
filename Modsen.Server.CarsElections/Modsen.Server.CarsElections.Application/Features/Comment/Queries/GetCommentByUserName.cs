using MediatR;

namespace Modsen.Server.CarsElections.Application.Features.Comment.Queries
{
    public record GetCommentByUserName : IRequest<Domain.Entities.Comment>
    {
        public string UserName { get; set; } = null!;
        public string CarId { get; set; } = null!;
    }
}
