using Modsen.Server.Authentication.Domain.Entities;
using System.Security.Claims;

namespace Modsen.Server.Authentication.Domain.Interfaces.Services
{
    public interface ITokenProviderService
    {
        string GenerateAccessToken(ApplicationUser user, List<string> roles);

        string GenerateAccessToken(IEnumerable<Claim> claims);
        
        string GenerateRefreshToken();
        
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
