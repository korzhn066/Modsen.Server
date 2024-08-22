using MediatR;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Commands;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class LoginApplicationUserHandler(LoginUserUseCase loginUserUseCase)
        : IRequestHandler<LoginApplicationUser, TokenModel>
    {
        private readonly LoginUserUseCase _loginUserUseCase = loginUserUseCase;

        public async Task<TokenModel> Handle(LoginApplicationUser request, CancellationToken cancellationToken)
        {
            return await _loginUserUseCase.LoginAsync(
                request.LoginModel,
                request.RefreshTokenValidityInDays);
        }
    }
}
