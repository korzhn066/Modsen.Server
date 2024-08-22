using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Authentication.Domain.Exceptions;
using Modsen.Server.Authentication.Domain.Exeptions;
using Modsen.Server.Authentication.Domain.Interfaces.Services;

namespace Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Commands
{
    public class RefreshTokenUseCase(
        ITokenProviderService tokenProviderService,
        UserManager<Domain.Entities.ApplicationUser> userManager)
    {
        private readonly ITokenProviderService _tokenProviderService = tokenProviderService;
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;

        public async Task<TokenModel> RefreshAsync(string oldAccessToken, string oldRefreshToken, int refreshTokenValidityInDays)
        {
            var principal = _tokenProviderService.GetPrincipalFromExpiredToken(oldAccessToken);

            var user = await _userManager.FindByNameAsync(principal.Identity!.Name!) ?? throw new NotFoundException();

            if (user.RefreshToken != oldRefreshToken)
            {
                throw new BadRequestException();
            }

            var newRefreshToken = _tokenProviderService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);

            return new TokenModel
            {
                RefreshToken = newRefreshToken,
                AccessToken = _tokenProviderService.GenerateAccessToken(principal.Claims)
            };
        }
    }
}
