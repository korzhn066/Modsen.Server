using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Infrastructure.Data.Seeds
{
    internal static class CarSeeder
    {
        internal static void SeedCars(this ModelBuilder builder)
        {
            var cars = new List<Car>
            {
                new() {
                    Id = "1"
                },
                new() {
                    Id = "2"
                }
            };

            builder.Entity<Car>().HasData(cars);
        }
    }
}
