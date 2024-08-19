using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Modsen.Server.Authentication.Application.Helpers;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Authentication.Domain.Interfaces.Services;
using System.Security.Authentication;

namespace Modsen.Server.Authentication.Application.UseCases.Authentication
{
    public class LoginUserUseCase
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager;
        private readonly ITokenProviderService _tokenProviderService;
        private readonly int _refreshTokenValidityInDays;

        public LoginUserUseCase(
            UserManager<Domain.Entities.ApplicationUser> userManager,
            ITokenProviderService tokenProviderService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenProviderService = tokenProviderService;
            _refreshTokenValidityInDays = ConfigurationHelper.GetRefreshTokenValidityInDays(configuration);
        }

        public async Task<TokenModel> LoginAsync(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName) ?? throw new InvalidCredentialException();
            var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);

            if (!result)
                throw new InvalidCredentialException();

            var roles = (List<string>)await _userManager.GetRolesAsync(user);

            var refreshToken = _tokenProviderService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_refreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);

            return new TokenModel
            {
                AccessToken = _tokenProviderService.GenerateAccessToken(user, roles),
                RefreshToken = refreshToken,
            };
        }
    }
}
