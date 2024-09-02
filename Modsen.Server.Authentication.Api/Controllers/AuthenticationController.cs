using Microsoft.AspNetCore.Mvc;
using Modsen.Server.Authentication.Api.Helpers;
using Modsen.Server.Authentication.Application.Helpers;
using Modsen.Server.Authentication.Application.Models.Authentication;
using MediatR;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;

namespace Modsen.Server.Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(
        IMediator mediator,
        IConfiguration configuration) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly int _refreshTokenValidityInDays = ConfigurationHelper.GetRefreshTokenValidityInDays(configuration);

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var tokenModel = await _mediator.Send(new RegisterApplicationUser
            {
                RegisterModel = registerModel,
                RefreshTokenValidityInDays = _refreshTokenValidityInDays
            });

            CookieHelper.SetRefreshTokenInCookie(tokenModel.RefreshToken, _refreshTokenValidityInDays, HttpContext);

            return Ok(tokenModel.AccessToken);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var tokenModel = await _mediator.Send(new LoginApplicationUser
            {
                LoginModel = loginModel,
                RefreshTokenValidityInDays = _refreshTokenValidityInDays
            });

            CookieHelper.SetRefreshTokenInCookie(tokenModel.RefreshToken, _refreshTokenValidityInDays, HttpContext);

            return Ok(tokenModel.AccessToken);
        }
    }
}
