using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Modsen.Server.Authentication.Domain.Entities;
using Modsen.Server.Authentication.Infrastructure.Data.Seeds;

namespace Modsen.Server.Authentication.Infrastructure.Data
{
    public class DBContext : IdentityDbContext<ApplicationUser>
    {
        public DBContext(DbContextOptions options): base(options) 
        {
            var dbCreater = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            
            if (dbCreater is not null)
            { 
                if (!dbCreater.CanConnect())
                {
                    dbCreater.Create();
                }

                if (!dbCreater.HasTables())
                {
                    dbCreater.CreateTables();
                }
            }
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
