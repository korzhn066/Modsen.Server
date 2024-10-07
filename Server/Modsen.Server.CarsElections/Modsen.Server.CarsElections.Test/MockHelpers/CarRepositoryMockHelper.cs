using Microsoft.EntityFrameworkCore;
using MockQueryable;
using MockQueryable.Moq;
using Modsen.Server.CarsElections.Domain.Entities;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;
using Modsen.Server.CarsElections.Infrastructure.Data;
using Modsen.Server.CarsElections.Infrastructure.Repositories;
using Modsen.Server.Shared.Extensions;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;

namespace Modsen.Server.CarsElections.Test.MockHelpers
{
    internal static class CarRepositoryMockHelper
    {
        public static ICarRepository MockCarRepositoryGetCars(List<Car> cars)
        {
            var carRepositoryMock = new Mock<ICarRepository>();

            carRepositoryMock
                .Setup(carRepository => carRepository.Query)
                .Returns(cars.BuildMock().BuildMockDbSet().Object);

            return carRepositoryMock.Object;
        }
    }
}
