using MediatR;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries
{
    public record GetApplicationUserByUsername : IRequest<Domain.Entities.ApplicationUser>
    {
        public string Username { get; set; } = null!;
    }
}
