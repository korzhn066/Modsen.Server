using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Domain.Entities;
using Modsen.Server.Authentication.Domain.Enums;
using Modsen.Server.Authentication.Domain.Interfaces.Services;
using Modsen.Server.Authentication.Domain.Models;

namespace Modsen.Server.Authentication.Application.Services
{
    public class ApplicationAuthenticateService : IApplicationAuthenticateService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationAuthenticateService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUser? User { get; private set; }

        public List<string> Roles { get; private set; } = new List<string>();   

        public async Task<bool> LoginAsync(LoginModel loginModel, string refreshToken,int refreshTokenValidityInDays)
        {
            User = await _userManager.FindByNameAsync(loginModel.UserName);
            
            if (User is null)
                return false;

            var result = await _userManager.CheckPasswordAsync(User, loginModel.Password);

            Roles = (List<string>)await _userManager.GetRolesAsync(User);

            User.RefreshToken = refreshToken;
            User.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(User);

            return result;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterModel registerModel, string refreshToken, int refreshTokenValidityInDays)
        { 
            User = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = registerModel.PhoneNumber,
                UserStatus = UserStatus.Unban,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenValidityInDays)
            };

            var result = await _userManager.CreateAsync(User, registerModel.Password);

            if (!result.Succeeded)
                return result;

            Roles = new List<string>() { "Client" }; 
            result = await _userManager.AddToRoleAsync(User, "Client");

            return result;
        }
    }
}
