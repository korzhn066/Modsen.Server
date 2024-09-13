using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;
using Modsen.Server.Authentication.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class RefreshTokenHandler(
        ITokenProviderService tokenProviderService,
        UserManager<Domain.Entities.ApplicationUser> userManager,
        ILogger<RefreshTokenHandler> logger)
        : IRequestHandler<RefreshToken, TokenModel>
    {
        private readonly ITokenProviderService _tokenProviderService = tokenProviderService;
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;
        private readonly ILogger<RefreshTokenHandler> _logger = logger;

        public async Task<TokenModel> Handle(RefreshToken request, CancellationToken cancellationToken)
        {
            var principal = _tokenProviderService.GetPrincipalFromExpiredToken(request.OldAccessToken);

            var user = await _userManager.FindByNameAsync(principal.Identity!.Name!);

            if (user is null)
            {
                _logger.LogError("User is null when trying to refresh token");

                throw new NotFoundException(ErrorConstants.NotFoundUserError);
            }

            if (user.RefreshToken != request.OldRefreshToken)
            {
                _logger.LogError("User has invalid refresh token");

                throw new BadRequestException(ErrorConstants.InvalidRefreshTokenError);
            }

            var newRefreshToken = _tokenProviderService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(request.RefreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);

            _logger.LogInformation("User refresh token");

            return new TokenModel
            {
                RefreshToken = newRefreshToken,
                AccessToken = _tokenProviderService.GenerateAccessToken(principal.Claims)
            };
        }
    }
}
