using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElection.Domain.Entities;

namespace Modsen.Server.CarsElection.Infrastructure.Data
{
    public class DBContext : DbContext 
    {
        public DbSet<User> Users { get; set; } 
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Car> Cars { get; set; }

        public DBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
        }
    }
}
