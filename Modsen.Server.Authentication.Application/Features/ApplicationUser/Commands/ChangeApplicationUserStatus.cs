using MediatR;
using Modsen.Server.Authentication.Domain.Enums;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands
{
    public record ChangeApplicationUserStatus : IRequest
    {
        public string UserId { get; set; } = null!;
        public UserStatus Status { get; set; }
    }
}
