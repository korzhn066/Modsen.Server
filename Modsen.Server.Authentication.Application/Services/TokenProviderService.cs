using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Modsen.Server.Authentication.Domain.Entities;
using Modsen.Server.Authentication.Domain.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Modsen.Server.Authentication.Application.Services
{
    public class TokenProviderService : ITokenProviderService
    {
        private readonly string _validAudience;
        private readonly string _validIssuer;
        private readonly string _secretKey;
        private readonly int _accessTokenValidityInMinutes;

        public TokenProviderService(IConfiguration configuration)
        {
            _validAudience = configuration["JWT:ValidAudience"] ?? throw new ArgumentNullException();
            _validIssuer = configuration["JWT:ValidIssuer"] ?? throw new ArgumentNullException();
            _secretKey = configuration["JWT:SecretKey"] ?? throw new ArgumentNullException();
            _accessTokenValidityInMinutes = GetAccessTokenValidityInMinutes(configuration);
        }

        public string GenerateAccessToken(ApplicationUser user, List<string> roles)
        {
            var tokeOptions = new JwtSecurityToken(
                issuer: _validIssuer,
                audience: _validAudience,
                claims: GetClaims(user, roles),
                expires: DateTime.Now.AddMinutes(_accessTokenValidityInMinutes),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var tokeOptions = new JwtSecurityToken(
                issuer: _validIssuer,
                audience: _validAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_accessTokenValidityInMinutes),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
                ValidateLifetime = false
            };

            var principal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out var securityToken);
            
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            
            if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            
            return principal;
        }


        private IEnumerable<Claim> GetClaims(ApplicationUser user, List<string> roles)
        {
            var authClaims = new List<Claim> 
            {
                new Claim(ClaimTypes.Name, user.UserName ?? throw new ArgumentNullException()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            return authClaims;
        }

        private int GetAccessTokenValidityInMinutes(IConfiguration configuration)
        {
            var parseResult = int.TryParse(configuration["JWT:AccessTokenValidityInMinutes"], out int accessTokenValidityInDays);

            if (!parseResult)
                throw new ArgumentException();

            return accessTokenValidityInDays;
        }
    }
}
