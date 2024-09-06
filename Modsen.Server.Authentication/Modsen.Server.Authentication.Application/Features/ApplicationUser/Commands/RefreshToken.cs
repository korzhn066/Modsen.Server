using MediatR;
using Modsen.Server.Authentication.Application.Models.Authentication;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands
{
    public class RefreshToken : IRequest<TokenModel>
    {
        public string OldRefreshToken { get; set; } = null!;
        public string OldAccessToken { get; set; } = null!;
        public int RefreshTokenValidityInDays { get; set; }
    }
}
