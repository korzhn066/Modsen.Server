namespace Modsen.Server.Authentication.Api.Helpers
{
    public class AuthenticateHelper
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
            var authorizationValue = httpContext.Request.Headers.Authorization[0];

            if (authorizationValue is null)
            {
                throw new ArgumentNullException();
            }

            return authorizationValue.Split(' ')[1];
        }
    }
}
