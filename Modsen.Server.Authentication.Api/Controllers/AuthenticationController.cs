using Microsoft.AspNetCore.Mvc;
using Modsen.Server.Authentication.Domain.Interfaces.Services;
using Modsen.Server.Authentication.Domain.Models;

namespace Modsen.Server.Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IApplicationAuthenticateService _applicationAuthenticateService;
        private readonly ITokenProviderService _tokenProviderService;
        private readonly int _refreshTokenValidityInDays;

        public AuthenticationController(
            IApplicationAuthenticateService applicationAuthenticateService, 
            ITokenProviderService tokenProviderService, 
            IConfiguration configuration) 
        { 
            _applicationAuthenticateService = applicationAuthenticateService;
            _tokenProviderService = tokenProviderService;
            _refreshTokenValidityInDays = GetRefreshTokenValidityInDays(configuration);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var refreshToken = _tokenProviderService.GenerateRefreshToken();

            var result = await _applicationAuthenticateService.RegisterAsync(registerModel, refreshToken, _refreshTokenValidityInDays);

            if (result.Succeeded) 
            { 
                SetRefreshTokenInCookie(refreshToken);

                return Ok(new 
                {
                    accessToken = _tokenProviderService.GenerateAccessToken(
                        _applicationAuthenticateService.User ?? throw new ArgumentNullException(),
                        _applicationAuthenticateService.Roles)
                });
            }

            return StatusCode(403, new
            {
                errors = result.Errors.Select(error => error.Code)
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var refreshToken = _tokenProviderService.GenerateRefreshToken();

            var result = await _applicationAuthenticateService.LoginAsync(loginModel, refreshToken, _refreshTokenValidityInDays);

            if (result)
            {
                SetRefreshTokenInCookie(refreshToken);

                return Ok(new
                {
                    accessToken = _tokenProviderService.GenerateAccessToken(
                        _applicationAuthenticateService.User ?? throw new ArgumentNullException(),
                        _applicationAuthenticateService.Roles)
                });
            }

            return Unauthorized();
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
