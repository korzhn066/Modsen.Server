using MediatR;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Commands;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class RegisterApplicationUserHandler(RegisterUserUseCase registerUserUseCase)
        : IRequestHandler<RegisterApplicationUser, TokenModel>
    {
        private readonly RegisterUserUseCase _registerUserUseCase = registerUserUseCase;

        public async Task<TokenModel> Handle(RegisterApplicationUser request, CancellationToken cancellationToken)
        {
            return await _registerUserUseCase.RegisterAsync(
                request.RegisterModel,
                request.RefreshTokenValidityInDays);
        }
    }
}
