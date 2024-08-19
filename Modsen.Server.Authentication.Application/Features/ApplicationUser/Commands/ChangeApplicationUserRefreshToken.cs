using MediatR;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands
{
    public record ChangeApplicationUserRefreshToken : IRequest
    {
        public string UserName { get; set; } = null!;
        public string? RefreshToken { get; set; } 
        public int RefreshTokenValidityInDays { get; set; }
    }
}