namespace Modsen.Server.Authentication.Api.Helpers
{
    public class CookieHelper
    {
        public static void SetRefreshTokenInCookie(
            string refreshToken, 
            int refreshTokenValidityInDays, 
            HttpContext htpContext)
        {
            htpContext.Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
            {
                MaxAge = TimeSpan.FromDays(refreshTokenValidityInDays)
            });
        }
    }
}
