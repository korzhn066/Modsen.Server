using Microsoft.AspNetCore.Http;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;

namespace Modsen.Server.Shared.Helpers
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
    }
}
