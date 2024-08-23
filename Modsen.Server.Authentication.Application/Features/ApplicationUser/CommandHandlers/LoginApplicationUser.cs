using MediatR;
using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Authentication.Domain.Exceptions;
using Modsen.Server.Authentication.Domain.Interfaces.Services;

namespace Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers
{
    public class LoginApplicationUserHandler(
        UserManager<Domain.Entities.ApplicationUser> userManager,
        ITokenProviderService tokenProviderService)
        : IRequestHandler<LoginApplicationUser, TokenModel>
    {
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager = userManager;
        private readonly ITokenProviderService _tokenProviderService = tokenProviderService;

        public async Task<TokenModel> Handle(LoginApplicationUser request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.LoginModel.UserName) ?? throw new BadRequestException();

            var result = await _userManager.CheckPasswordAsync(user, request.LoginModel.Password);

            if (!result)
            {
                throw new BadRequestException();
            }

            var roles = (List<string>)await _userManager.GetRolesAsync(user);

            var refreshToken = _tokenProviderService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(request.RefreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);

            return new TokenModel
            {
                AccessToken = _tokenProviderService.GenerateAccessToken(user, roles),
                RefreshToken = refreshToken,
            };
        }
    }
}
