using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.Authentication.Api.Helpers;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Helpers;
using Modsen.Server.Shared.Helpers;

namespace Modsen.Server.Authentication.Api.Controllers
{
    [Route("api/tokens/")]
    [ApiController]
    public class TokenController(
        IConfiguration configuration,
        IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly int _refreshTokenValidityInDays = ConfigurationHelper.GetRefreshTokenValidityInDays(configuration);

        [HttpGet]
        public async Task<IActionResult> Refresh()
        {
            var tokenModel = await _mediator.Send(new RefreshToken
            {
                OldAccessToken = TokenHelper.GetAccessToken(HttpContext),
                OldRefreshToken = TokenHelper.GetRefreshToken(HttpContext),
                RefreshTokenValidityInDays = _refreshTokenValidityInDays
            });

            CookieHelper.SetRefreshTokenInCookie(tokenModel.RefreshToken, _refreshTokenValidityInDays, HttpContext);

            return Ok(tokenModel.AccessToken);
        }
        
        [HttpDelete, Authorize]
        public async Task<IActionResult> Revoke()
        {
            await _mediator.Send(new ChangeApplicationUserRefreshToken
            {
                UserName = AuthenticateHelper.GetUserName(HttpContext),
                RefreshToken = null,
            });

            return NoContent();
        }
    }
}
