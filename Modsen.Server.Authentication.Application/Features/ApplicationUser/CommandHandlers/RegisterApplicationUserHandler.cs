using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Authentication.Domain.Constants;
using Modsen.Server.Authentication.Domain.Enums;
using Modsen.Server.Authentication.Domain.Exceptions;
using Modsen.Server.Authentication.Domain.Interfaces.Services;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class RegisterApplicationUserHandler(
        UserManager<Domain.Entities.ApplicationUser> userManager,
        ITokenProviderService tokenProviderService)
        : IRequestHandler<RegisterApplicationUser, TokenModel>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;
        private readonly ITokenProviderService _tokenProviderService = tokenProviderService;

        public async Task<TokenModel> Handle(RegisterApplicationUser request, CancellationToken cancellationToken)
        {
            var refreshToken = _tokenProviderService.GenerateRefreshToken();

            var user = new Domain.Entities.ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = request.RegisterModel.PhoneNumber,
                UserName = request.RegisterModel.UserName,
                UserStatus = UserStatus.Unban,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(request.RefreshTokenValidityInDays)
            };

            var result = await _userManager.CreateAsync(user, request.RegisterModel.Password);

            if (!result.Succeeded)
            {
                throw new BadRequestException(string.Join(' ', result.Errors.Select(error => error.Code)));
            }

            result = await _userManager.AddToRoleAsync(user, "Client");

            if (!result.Succeeded)
            {
                throw new DbUpdateException(ErrorConstants.ServerSideError);
            }

            return new TokenModel
            {
                AccessToken = _tokenProviderService.GenerateAccessToken(user, ["Client"]),
                RefreshToken = refreshToken
            };
        }
    }
}
