using Microsoft.Extensions.Logging;
using Modsen.Server.CarsElections.Application.Features.Car.Queries;
using Modsen.Server.CarsElections.Application.Features.Car.QueryHandlers;
using Modsen.Server.CarsElections.Test.MockHelpers;
using Modsen.Server.Shared.Enums;
using Modsen.Server.Shared.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElections.Test.Features.Car.QueryHandlers
{
    public class GetCurrentWinningCarHandlerUnitTest
    {
        [Fact]
        public async Task ReturnCarFromCache()
        {
            var car = new Domain.Entities.Car
            {
                Id = "1"
            };

            var loggerMock = new Mock<ILogger<GetCurrentWinningCarHandler>>().Object;
            var cacheRepositoryMock = CacheRepositoryMockHelper.MockCacheRepositoryGetEntity(car);
            var carRepositoryMock = CarRepositoryMockHelper.MockCarRepositoryGetCars([]);

            var getCurrentWinningCarHandler = new GetCurrentWinningCarHandler(
                carRepositoryMock,
                cacheRepositoryMock,
                loggerMock);

            var result = await getCurrentWinningCarHandler.Handle(new GetCurrentWinningCar(), CancellationToken.None);

            Assert.Equal(car, result);
        }

        [Fact]
        public void ReturnNotFoundException()
        {
            var loggerMock = new Mock<ILogger<GetCurrentWinningCarHandler>>().Object;
            var cacheRepositoryMock = CacheRepositoryMockHelper.MockCacheRepositoryGetEntity<Domain.Entities.Car>(null);
            var carRepositoryMock = CarRepositoryMockHelper.MockCarRepositoryGetCars([]);

            var getCurrentWinningCarHandler = new GetCurrentWinningCarHandler(
                carRepositoryMock,
                cacheRepositoryMock,
                loggerMock);

            Task act()
            {
                return getCurrentWinningCarHandler.Handle(new GetCurrentWinningCar(), CancellationToken.None);
            }

            Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async Task ReturnWinningCar()
        {
            var cars = new List<Domain.Entities.Car>
            {
                new()
                {
                    Id = "1",
                    CarType = CarType.Elections
                }
            };

            var loggerMock = new Mock<ILogger<GetCurrentWinningCarHandler>>().Object;
            var cacheRepositoryMock = CacheRepositoryMockHelper.MockCacheRepositoryGetEntity<Domain.Entities.Car>(null);
            var carRepositoryMock = CarRepositoryMockHelper.MockCarRepositoryGetCars(cars);

            var getCurrentWinningCarHandler = new GetCurrentWinningCarHandler(
                carRepositoryMock,
                cacheRepositoryMock,
                loggerMock);

            var result = await getCurrentWinningCarHandler.Handle(new GetCurrentWinningCar(), CancellationToken.None);

            Assert.Equal("1", result.Id);
        }
    }
}
