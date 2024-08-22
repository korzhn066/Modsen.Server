using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Authentication.Domain.Entities;
using Modsen.Server.Authentication.Domain.Enums;
using Modsen.Server.Authentication.Domain.Exceptions;
using Modsen.Server.Authentication.Domain.Interfaces.Services;
using System.Data;

namespace Modsen.Server.Authentication.Application.UseCases.ApplicationUser.Commands
{
    public class RegisterUserUseCase(
        UserManager<Domain.Entities.ApplicationUser> userManager,
        ITokenProviderService tokenProviderService)
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;
        private readonly ITokenProviderService _tokenProviderService = tokenProviderService;

        public async Task<TokenModel> RegisterAsync(RegisterModel registerModel, int refreshTokenValidityInDays)
        {
            var refreshToken = _tokenProviderService.GenerateRefreshToken();

            var user = new Domain.Entities.ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = registerModel.PhoneNumber,
                UserName = registerModel.UserName,
                UserStatus = UserStatus.Unban,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenValidityInDays)
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
            {
                throw new BadRequestException(string.Join(' ', result.Errors.Select(error => error.Code)));
            }

            result = await _userManager.AddToRoleAsync(user, "Client");

            if (!result.Succeeded)
            {
                throw new DbUpdateException("Server side erroe");
            }

            return new TokenModel
            {
                AccessToken = _tokenProviderService.GenerateAccessToken(user, ["Client"]),
                RefreshToken = refreshToken
            };
        }
    }
}
