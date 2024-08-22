using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.Authentication.Api.Helpers;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Helpers;

namespace Modsen.Server.Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController(
        IConfiguration configuration,
        IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly int _refreshTokenValidityInDays = ConfigurationHelper.GetRefreshTokenValidityInDays(configuration);

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh()
        {
            try
            {
                var tokenModel = await _mediator.Send(new RefreshToken
                {
                    OldAccessToken = AuthenticateHelper.GetAccessToken(HttpContext),
                    OldRefreshToken = AuthenticateHelper.GetRefreshToken(HttpContext),
                    RefreshTokenValidityInDays = _refreshTokenValidityInDays 
                });

                CookieHelper.SetRefreshTokenInCookie(tokenModel.RefreshToken, _refreshTokenValidityInDays, HttpContext);

                return Ok(new
                {
                    accessToken = tokenModel.AccessToken
                });
            }
            catch
            {
                return Unauthorized();
            }
        }
        
        [HttpPost, Authorize]
        [Route("revoke")]
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
