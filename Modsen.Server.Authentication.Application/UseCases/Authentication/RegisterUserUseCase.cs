using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modsen.Server.Authentication.Application.Helpers;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Authentication.Domain.Entities;
using Modsen.Server.Authentication.Domain.Enums;
using Modsen.Server.Authentication.Domain.Interfaces.Services;
using System.Data;

namespace Modsen.Server.Authentication.Application.UseCases.Authentication
{
    public class RegisterUserUseCase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenProviderService _tokenProviderService;
        private readonly int _refreshTokenValidityInDays;

        public RegisterUserUseCase(
            UserManager<ApplicationUser> userManager,
            ITokenProviderService tokenProviderService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenProviderService = tokenProviderService;
            _refreshTokenValidityInDays = ConfigurationHelper.GetRefreshTokenValidityInDays(configuration);
        }

        public async Task<TokenModel> RegisterAsync(RegisterModel registerModel)
        {
            var refreshToken = _tokenProviderService.GenerateRefreshToken();

            var user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = registerModel.PhoneNumber,
                UserStatus = UserStatus.Unban,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_refreshTokenValidityInDays)
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
            {
                throw new InvalidDataException(string.Join(' ', result.Errors.Select(error => error.Code)));
            }

            result = await _userManager.AddToRoleAsync(user, "Client");

            if (!result.Succeeded)
            {
                throw new DbUpdateException("Server side erroe");
            }

            return new TokenModel
            {
                AccessToken = _tokenProviderService.GenerateAccessToken(user, new List<string> { "Client" }),
                RefreshToken = refreshToken
            };
        }
    }
}
