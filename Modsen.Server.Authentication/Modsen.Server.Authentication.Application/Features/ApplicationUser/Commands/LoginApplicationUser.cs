using MediatR;
using Modsen.Server.Authentication.Application.Models.Authentication;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands
{
    public class LoginApplicationUser : IRequest<TokenModel>
    {
        public LoginModel LoginModel { get; set; } = null!;
        public int RefreshTokenValidityInDays { get; set; }
    }
}
