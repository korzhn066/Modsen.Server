using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElection.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElection.Infrastructure.Data.Seeds
{
    internal static class CarSeeder
    {
        internal static void SeedCars(this ModelBuilder builder)
        {
            var cars = new List<Car> 
            { 
                new Car
                {
                    Id = "1"
                }
            };

            builder.Entity<Car>().HasData(cars);
        }
    }
}
