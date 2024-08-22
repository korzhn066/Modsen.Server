using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Authentication.Domain.Exceptions;
using Modsen.Server.Authentication.Domain.Interfaces.Services;

namespace Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Commands
{
    public class LoginUserUseCase(
        UserManager<Domain.Entities.ApplicationUser> userManager,
        ITokenProviderService tokenProviderService)
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;
        private readonly ITokenProviderService _tokenProviderService = tokenProviderService;

        public async Task<TokenModel> LoginAsync(LoginModel loginModel, int refreshTokenValidityInDays)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName) ?? throw new BadRequestException();

            var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);

            if (!result)
            {
                throw new BadRequestException();
            }

            var roles = (List<string>)await _userManager.GetRolesAsync(user);

            var refreshToken = _tokenProviderService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);

            return new TokenModel
            {
                AccessToken = _tokenProviderService.GenerateAccessToken(user, roles),
                RefreshToken = refreshToken,
            };
        }
    }
}
