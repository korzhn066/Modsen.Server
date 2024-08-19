using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.QueryHandlers
{
    public class CheckApplicationUserRefreshTokenValidityHandler : IRequestHandler<CheckApplicationUserRefreshTokenValidity, bool>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager;

        public CheckApplicationUserRefreshTokenValidityHandler(UserManager<Domain.Entities.ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(CheckApplicationUserRefreshTokenValidity request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user is null) 
                throw new KeyNotFoundException(nameof(user));

            if (user.RefreshToken != request.RefreshToken || 
                user.RefreshTokenExpiryTime < DateTime.Now)
                return false;
            else 
                return true;
        }
    }
}
