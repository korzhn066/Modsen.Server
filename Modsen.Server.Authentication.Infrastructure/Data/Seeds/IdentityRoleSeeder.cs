using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Modsen.Server.Authentication.Infrastructure.Data.Seeds
{
    internal static class IdentityRoleSeeder
    {
        internal static void SeedIdentityRoles(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole>()
                .HasData(
                    new()
                    {
                        Id = "1e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Client",
                        NormalizedName = "CLIENT",
                    },
                    new()
                    {
                        Id = "2e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                    }
                );
        }
    }
}
