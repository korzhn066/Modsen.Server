using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;

namespace Modsen.Server.Authentication.Api.Helpers
{
    public static class TokenHelper
    {
        public static string GetAccessToken(HttpContext httpContext)
        {
            var authorizationValue = httpContext.Request.Headers.Authorization[0]
                ?? throw new NotFoundException(ErrorConstants.NotFoundAccessTokenError);

            return authorizationValue.Split(' ')[1];
        }

        public static string GetRefreshToken(HttpContext httpContext)
        {
            return httpContext.Request.Cookies["RefreshToken"]
                ?? throw new NotFoundException(ErrorConstants.NotFoundRefreshTokenError);
        }
    }
}
