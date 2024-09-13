using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class ChangeApplicationUserRefreshTokenHandler(
        UserManager<Domain.Entities.ApplicationUser> userManager,
        ILogger<ChangeApplicationUserRefreshTokenHandler> logger)
        : IRequestHandler<ChangeApplicationUserRefreshToken>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;
        private readonly ILogger<ChangeApplicationUserRefreshTokenHandler> _logger = logger;

        public async Task Handle(ChangeApplicationUserRefreshToken request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user is null)
            {
                _logger.LogError("User is null when trying to refresh token");

                throw new NotFoundException(ErrorConstants.NotFoundUserError);
            }

            user.RefreshToken = request.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(request.RefreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);

            _logger.LogInformation("User refresh token");
        }
    }
}
