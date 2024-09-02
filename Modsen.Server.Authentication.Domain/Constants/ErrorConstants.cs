using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Domain.Constants
{
    public static class ErrorConstants
    {
        public const string NotFoundAccessTokenError = "NotFoundAccessToken";
        public const string NotFoundRefreshTokenError = "NotFoundRefreshToken";
        public const string NotFoundUserError = "NotFoundUser";
        public const string LoginError = "CheckLoginAndPassword";
        public const string ServerSideError = "ServerSideError";
        public const string InvalidRefreshTokenError = "InvalidRefreshToken";
    }
}
