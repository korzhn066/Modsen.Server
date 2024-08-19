using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.Authentication.Domain.Entities;
using Modsen.Server.Authentication.Infrastructure.Data.Seeds;

namespace Modsen.Server.Authentication.Infrastructure.Data
{
    public class DBContext : IdentityDbContext<ApplicationUser>
    {
        public DBContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.SeedApplicationUsers();
            builder.SeedIdentityRoles();
            builder.SeedIdentityUserRoles();

            base.OnModelCreating(builder);
        }
    }
}
