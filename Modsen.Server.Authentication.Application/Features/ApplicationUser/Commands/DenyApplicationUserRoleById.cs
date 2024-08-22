using MediatR;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands
{
    public record DenyApplicationUserRoleById : IRequest
    {
        public string UserId { get; set; } = null!;
        public string RoleName { get; set; } = null!;
    }
}
