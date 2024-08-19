using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using Modsen.Server.Authentication.Application.Services;
using Modsen.Server.Authentication.Domain.Interfaces.Services;

namespace Modsen.Server.Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenProviderService _tokenProviderService;
        private readonly IMediator _mediator;
        private readonly int _refreshTokenValidityInDays;

        public TokenController(
            ITokenProviderService tokenProviderService,
            IConfiguration configuration, 
            IMediator mediator)
        {
            _tokenProviderService = tokenProviderService;
            _mediator = mediator;
            _refreshTokenValidityInDays = GetRefreshTokenValidityInDays(configuration);
        }

        
        [HttpPost]
        [Route("refresh")]
        
        public async Task<IActionResult> Refresh()
        {
            var oldAccesstoken = HttpContext.Request.Headers.Authorization[0].Split(' ')[1];
            var principal = _tokenProviderService.GetPrincipalFromExpiredToken(oldAccesstoken);

            var result = await _mediator.Send(new CheckApplicationUserRefreshTokenValidity
            {
                UserName = principal.Identity!.Name!,
                RefreshToken = HttpContext.Request.Cookies["RefreshToken"]!
            });

            if (!result)
                return StatusCode(401, "Invalid refresh token");

            var newRefreshToken = _tokenProviderService.GenerateRefreshToken();

            await _mediator.Send(new ChangeApplicationUserRefreshToken
            {
                UserName = principal.Identity!.Name!,
                RefreshToken = newRefreshToken,
                RefreshTokenValidityInDays = _refreshTokenValidityInDays
            });

            SetRefreshTokenInCookie(newRefreshToken);

            return Ok(new
            {
                accessToken = _tokenProviderService.GenerateAccessToken(principal.Claims)
            });
        }

        
        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            await _mediator.Send(new ChangeApplicationUserRefreshToken
            {
                UserName = HttpContext.User.Identity.Name,
                RefreshToken = null,
            });

            return Ok();
        }

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            HttpContext.Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
            {
                MaxAge = TimeSpan.FromDays(_refreshTokenValidityInDays)
            });
        }

        private int GetRefreshTokenValidityInDays(IConfiguration configuration)
        {
            var parseResult = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            if (!parseResult)
                throw new ArgumentException();

            return refreshTokenValidityInDays;
        }
    }
}
