using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Application.Helpers
{
    public class ConfigurationHelper
    {
        public static int GetRefreshTokenValidityInDays(IConfiguration configuration)
        {
            var parseResult = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            if (!parseResult)
                throw new ArgumentException();

            return refreshTokenValidityInDays;
        }
    }
}
