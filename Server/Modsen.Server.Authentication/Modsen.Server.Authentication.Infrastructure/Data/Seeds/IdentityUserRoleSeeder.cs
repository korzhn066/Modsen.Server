using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Modsen.Server.Authentication.Infrastructure.Data.Seeds
{
    internal static class IdentityUserRoleSeeder
    {
        internal static void SeedIdentityUserRoles(this ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>()
                .HasData(
                    new IdentityUserRole<string>()
                    {
                        UserId = "3e445865-a24d-4543-a6c6-9443d048cdb9",
                        RoleId = "1e445865-a24d-4543-a6c6-9443d048cdb9",
                    },
                    new IdentityUserRole<string>()
                    {
                        UserId = "3e445865-a24d-4543-a6c6-9443d048cdb9",
                        RoleId = "2e445865-a24d-4543-a6c6-9443d048cdb9",
                    },
                    new IdentityUserRole<string>()
                    {
                        UserId = "4e445865-a24d-4543-a6c6-9443d048cdb9",
                        RoleId = "1e445865-a24d-4543-a6c6-9443d048cdb9",
                    }
                );
        }
    }
}
