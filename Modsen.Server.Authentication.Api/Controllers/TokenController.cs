using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.Authentication.Api.Helpers;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Helpers;
using Modsen.Server.Authentication.Application.UseCases.Authentication;

namespace Modsen.Server.Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RefreshTokenUseCase _refreshTokenUseCase;
        private readonly int _refreshTokenValidityInDays;

        public TokenController(
            IConfiguration configuration, 
            RefreshTokenUseCase refreshTokenUseCase,
            IMediator mediator)
        {
            _mediator = mediator;
            _refreshTokenUseCase = refreshTokenUseCase;
            _refreshTokenValidityInDays = ConfigurationHelper.GetRefreshTokenValidityInDays(configuration);
        }

        
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh()
        {
            try
            {
                var tokenModel = await _refreshTokenUseCase.RefreshAsync(
                    AuthenticateHelper.GetAccessToken(HttpContext), 
                    HttpContext.Request.Cookies["RefreshToken"]!);

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
