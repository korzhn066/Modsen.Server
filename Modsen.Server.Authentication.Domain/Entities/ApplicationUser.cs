using Microsoft.AspNetCore.Identity;
using Modsen.Server.Authentication.Domain.Enums;

namespace Modsen.Server.Authentication.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public UserStatus UserStatus { get; set; } = UserStatus.Unban;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
