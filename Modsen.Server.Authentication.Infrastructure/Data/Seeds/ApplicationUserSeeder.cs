using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.Authentication.Domain.Entities;
using Modsen.Server.Authentication.Domain.Enums;

namespace Modsen.Server.Authentication.Infrastructure.Data.Seeds
{
    internal static class ApplicationUserSeeder
    {
        internal static void SeedApplicationUsers(this ModelBuilder builder)
        {
            var users = new List<ApplicationUser>() {
                new()
                {
                    Id = "3e445865-a24d-4543-a6c6-9443d048cdb9",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN".ToUpper(),
                    UserStatus = UserStatus.Unban,
                    PhoneNumber = "+375336121213",
                },
                new()
                {
                    Id = "4e445865-a24d-4543-a6c6-9443d048cdb9",
                    UserStatus = UserStatus.Unban,
                    UserName = "user1",
                    NormalizedUserName = "USER1".ToUpper(),
                    PhoneNumber = "+375336121213",
                }
            };

            var hasher = new PasswordHasher<ApplicationUser>();

            users[0].PasswordHash = hasher.HashPassword(users[0], "Admin1_admin");
            users[1].PasswordHash = hasher.HashPassword(users[1], "User1_user");

            builder.Entity<ApplicationUser>().HasData(users);
        }
    }
}
