using Modsen.Server.Authentication.Domain.Exceptions;

namespace Modsen.Server.Authentication.Api.Helpers
{
    public static class AuthenticateHelper
    {
        public static string GetUserName(HttpContext httpContext)
        {
            ArgumentNullException.ThrowIfNull(httpContext);
            ArgumentNullException.ThrowIfNull(httpContext.User);
            ArgumentNullException.ThrowIfNull(httpContext.User.Identity);
            ArgumentNullException.ThrowIfNull(httpContext.User.Identity.Name);

            return httpContext.User.Identity.Name;
        }

        public static string GetAccessToken(HttpContext httpContext)
        {
            var authorizationValue = httpContext.Request.Headers.Authorization[0] ?? throw new BadRequestException();

            return authorizationValue.Split(' ')[1];
        }

        public static string GetRefreshToken(HttpContext httpContext)
        {
            return httpContext.Request.Cookies["RefreshToken"] ?? throw new BadRequestException();
        }
    }
}
