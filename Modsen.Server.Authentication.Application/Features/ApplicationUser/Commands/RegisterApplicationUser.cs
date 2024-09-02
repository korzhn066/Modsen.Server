using MediatR;
using Modsen.Server.Authentication.Application.Models.Authentication;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands
{
    public class RegisterApplicationUser : IRequest<TokenModel>
    {
        public RegisterModel RegisterModel { get; set; } = null!;
        public int RefreshTokenValidityInDays { get;set; }
    }
}
