using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;
using Modsen.Server.Authentication.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class LoginApplicationUserHandler(
        UserManager<Domain.Entities.ApplicationUser> userManager,
        ITokenProviderService tokenProviderService,
        ILogger<LoginApplicationUser> logger)
        : IRequestHandler<LoginApplicationUser, TokenModel>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;
        private readonly ITokenProviderService _tokenProviderService = tokenProviderService;
        private readonly ILogger<LoginApplicationUser> _logger = logger;

        public async Task<TokenModel> Handle(LoginApplicationUser request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.LoginModel.UserName);

            if (user is null)
            {
                _logger.LogError("Incorrect user name");

                throw new BadRequestException(ErrorConstants.LoginError);
            }

            var result = await _userManager.CheckPasswordAsync(user, request.LoginModel.Password);

            if (!result)
            {
                _logger.LogError("Incorrect password");

                throw new BadRequestException(ErrorConstants.LoginError);
            }

            var roles = (List<string>)await _userManager.GetRolesAsync(user);

            var refreshToken = _tokenProviderService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(request.RefreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);

            _logger.LogInformation("User login");

            return new TokenModel
            {
                AccessToken = _tokenProviderService.GenerateAccessToken(user, roles),
                RefreshToken = refreshToken,
            };
        }
    }
}
