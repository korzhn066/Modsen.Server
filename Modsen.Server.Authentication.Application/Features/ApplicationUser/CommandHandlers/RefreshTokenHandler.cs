using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Authentication.Domain.Constants;
using Modsen.Server.Authentication.Domain.Exceptions;
using Modsen.Server.Authentication.Domain.Exeptions;
using Modsen.Server.Authentication.Domain.Interfaces.Services;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class RefreshTokenHandler(
        ITokenProviderService tokenProviderService,
        UserManager<Domain.Entities.ApplicationUser> userManager)
        : IRequestHandler<RefreshToken, TokenModel>
    {
        private readonly ITokenProviderService _tokenProviderService = tokenProviderService;
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;

        public async Task<TokenModel> Handle(RefreshToken request, CancellationToken cancellationToken)
        {
            var principal = _tokenProviderService.GetPrincipalFromExpiredToken(request.OldAccessToken);

            var user = await _userManager.FindByNameAsync(principal.Identity!.Name!) 
                ?? throw new NotFoundException(ErrorConstants.NotFoundUserError);

            if (user.RefreshToken != request.OldRefreshToken)
            {
                throw new BadRequestException(ErrorConstants.InvalidRefreshTokenError);
            }

            var newRefreshToken = _tokenProviderService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(request.RefreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);

            return new TokenModel
            {
                RefreshToken = newRefreshToken,
                AccessToken = _tokenProviderService.GenerateAccessToken(principal.Claims)
            };
        }
    }
}
