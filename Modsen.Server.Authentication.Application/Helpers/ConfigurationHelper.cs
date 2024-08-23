using Microsoft.Extensions.Configuration;
using Modsen.Server.Authentication.Domain.Constants;
using Modsen.Server.Authentication.Domain.Exceptions;

namespace Modsen.Server.Authentication.Application.Helpers
{
    public static class ConfigurationHelper
    {
        public static int GetRefreshTokenValidityInDays(IConfiguration configuration)
        {
            var parseResult = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            if (parseResult)
            {
                return refreshTokenValidityInDays;
            }

            throw new ArgumentException(ErrorConstants.ServerSideError);
        }
    }
}
