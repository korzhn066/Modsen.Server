using MediatR;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands
{
    public record AddApplicationUserToRoleById : IRequest
    {
        public string RoleName { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
}
