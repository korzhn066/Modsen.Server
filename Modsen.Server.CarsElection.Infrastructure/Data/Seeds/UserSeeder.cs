using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElection.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElection.Infrastructure.Data.Seeds
{
    internal static class UserSeeder
    {
        internal static void SeedUsers(this ModelBuilder builder)
        {
            var users = new List<User> 
            { 
                new User
                {
                    Id = "1",
                    UserName = "Test",
                },
                new User
                {
                    Id = "2",
                    UserName = "Test",
                },
                new User
                {
                    Id = "3",
                    UserName = "Test",
                }
            };

            builder.Entity<User>().HasData(users);
        }
    }
}
