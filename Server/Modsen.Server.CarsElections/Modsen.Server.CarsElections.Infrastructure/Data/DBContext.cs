using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Modsen.Server.CarsElections.Domain.Entities;
using Modsen.Server.CarsElections.Infrastructure.Data.Configuration;
using Modsen.Server.CarsElections.Infrastructure.Data.Seeds;

namespace Modsen.Server.CarsElections.Infrastructure.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
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

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ConfigureLikes();

            builder.SeedCars();
            builder.SeedUsers();
            builder.SeedComments();
            builder.SeedLikes();

            base.OnModelCreating(builder);
        }
    }
}
