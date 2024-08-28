using MediatR;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries
{
    public record GetApplicationUsers : IRequest<List<Domain.Entities.ApplicationUser>>
    {
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
