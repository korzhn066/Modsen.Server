using MediatR;
using Microsoft.Extensions.Configuration;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using Modsen.Server.Authentication.Application.Helpers;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Authentication.Domain.Interfaces.Services;
using System.Security.Authentication;

namespace Modsen.Server.Authentication.Application.UseCases.Authentication
{
    public class RefreshTokenUseCase
    {
        private readonly ITokenProviderService _tokenProviderService;
        private readonly IMediator _mediator;
        private readonly int _refreshTokenValidityInDays;

        public RefreshTokenUseCase(
            ITokenProviderService tokenProviderService,
            IMediator mediator,
            IConfiguration configuration)
        {
            _tokenProviderService = tokenProviderService;
            _mediator = mediator;
            _refreshTokenValidityInDays = ConfigurationHelper.GetRefreshTokenValidityInDays(configuration);
        }

        public async Task<TokenModel> RefreshAsync(string oldAccessToken, string oldRefreshToken)
        {
            var principal = _tokenProviderService.GetPrincipalFromExpiredToken(oldAccessToken);

            var result = await _mediator.Send(new CheckApplicationUserRefreshTokenValidity
            {
                UserName = principal.Identity!.Name!,
                RefreshToken = oldRefreshToken
            });

            if (!result)
                throw new InvalidCredentialException();

            var newRefreshToken = _tokenProviderService.GenerateRefreshToken();

            await _mediator.Send(new ChangeApplicationUserRefreshToken
            {
                UserName = principal.Identity!.Name!,
                RefreshToken = newRefreshToken,
                RefreshTokenValidityInDays = _refreshTokenValidityInDays
            });

            return new TokenModel
            {
                RefreshToken = newRefreshToken,
                AccessToken = _tokenProviderService.GenerateAccessToken(principal.Claims)
            };
        }
    }
}
