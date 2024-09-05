using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class ChangeApplicationUserRefreshTokenHandler(UserManager<Domain.Entities.ApplicationUser> userManager)
        : IRequestHandler<ChangeApplicationUserRefreshToken>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;

        public async Task Handle(ChangeApplicationUserRefreshToken request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName) 
                ?? throw new NotFoundException(ErrorConstants.NotFoundUserError);

            user.RefreshToken = request.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(request.RefreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);
        }
    }
}
