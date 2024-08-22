using MediatR;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Commands;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class RefreshTokenHandler(RefreshTokenUseCase refreshTokenUseCase) 
        : IRequestHandler<RefreshToken, TokenModel>
    {
        private readonly RefreshTokenUseCase _refreshTokenUseCase = refreshTokenUseCase;

        public async Task<TokenModel> Handle(RefreshToken request, CancellationToken cancellationToken)
        {
            return await _refreshTokenUseCase.RefreshAsync(
                request.OldAccessToken,
                request.OldRefreshToken,
                request.RefreshTokenValidityInDays);
        }
    }
}
