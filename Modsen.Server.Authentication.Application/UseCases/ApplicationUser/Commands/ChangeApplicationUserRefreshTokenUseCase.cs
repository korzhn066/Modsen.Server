using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Domain.Exeptions;

namespace Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Commands
{
    public class ChangeApplicationUserRefreshTokenUseCase(UserManager<Domain.Entities.ApplicationUser> userManager)
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;

        public async Task ChangeRefreshTokenAsync(string userName, string? refreshToken, int refreshTokenValidityInDays)
        {
            var user = await _userManager.FindByNameAsync(userName) ?? throw new NotFoundException();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);
        }
    }
}
