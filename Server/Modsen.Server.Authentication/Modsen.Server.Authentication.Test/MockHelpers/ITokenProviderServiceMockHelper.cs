using Modsen.Server.Authentication.Domain.Interfaces.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Test.MockHelpers
{
    internal static class ITokenProviderServiceMockHelper
    {
        public static ITokenProviderService MockTokenProviderService(string refreshToken)
        {
            var mockTokenProviderService = new Mock<ITokenProviderService>();

            mockTokenProviderService
                .Setup(tokenProviderService => tokenProviderService.GenerateRefreshToken())
                .Returns(refreshToken);

            var claims = new List<Claim>()
            {
                new (ClaimTypes.Name, "username"),
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            mockTokenProviderService
                .Setup(ITokenProviderService => ITokenProviderService.GetPrincipalFromExpiredToken(It.IsAny<string>()))
                .Returns(claimsPrincipal);

            return mockTokenProviderService.Object;
        }
    }
}
