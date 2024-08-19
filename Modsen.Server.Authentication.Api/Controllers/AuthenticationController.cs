using Microsoft.AspNetCore.Mvc;
using Modsen.Server.Authentication.Api.Helpers;
using Modsen.Server.Authentication.Application.Helpers;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Authentication.Application.UseCases.Authentication;

namespace Modsen.Server.Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private LoginUserUseCase _loginUserUseCase;
        private RegisterUserUseCase _registerUserUseCase;
        private int _refreshTokenValidityInDays;

        public AuthenticationController(
            LoginUserUseCase loginUserUseCase, 
            RegisterUserUseCase registerUserUseCase,
            IConfiguration configuration) 
        { 
            _loginUserUseCase = loginUserUseCase;
            _registerUserUseCase= registerUserUseCase;
            _refreshTokenValidityInDays = ConfigurationHelper.GetRefreshTokenValidityInDays(configuration);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            try
            {
                var tokenModel = await _registerUserUseCase.RegisterAsync(registerModel);

                CookieHelper.SetRefreshTokenInCookie(tokenModel.RefreshToken, _refreshTokenValidityInDays, HttpContext);

                return Ok(new
                {
                    accessToken = tokenModel.AccessToken
                });
            }
            catch (Exception ex) 
            {
                return StatusCode(403, new
                {
                    errors = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try
            {
                var tokenModel = await _loginUserUseCase.LoginAsync(loginModel);

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
    }
}
