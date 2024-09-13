using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Infrastructure.Data.Seeds
{
    internal static class UserSeeder
    {
        internal static void SeedUsers(this ModelBuilder builder)
        {
            var users = new List<User>
            {
                new() {
                    Id = "1",
                    UserName = "Test",
                },
                new() {
                    Id = "2",
                    UserName = "Test1",
                },
                new() {
                    Id = "3",
                    UserName = "Test2",
                }
            };

            builder.Entity<User>().HasData(users);
        }
    }
}
