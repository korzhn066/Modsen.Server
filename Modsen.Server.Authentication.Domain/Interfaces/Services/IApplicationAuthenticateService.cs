using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Domain.Entities;
using Modsen.Server.Authentication.Domain.Models;

namespace Modsen.Server.Authentication.Domain.Interfaces.Services
{
    public interface IApplicationAuthenticateService
    {
        List<string> Roles { get; }
        ApplicationUser? User { get; }
        Task<bool> LoginAsync(LoginModel loginModel, string refreshToken, int refreshTokenValidityInDays);
        Task<IdentityResult> RegisterAsync(RegisterModel registerModel, string refreshToken, int refreshTokenValidityInDays);
    }
}
