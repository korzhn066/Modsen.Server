using Microsoft.Extensions.Logging;
using Modsen.Server.CarsElections.Application.Features.Car.Queries;
using Modsen.Server.CarsElections.Application.Features.Car.QueryHandlers;
using Modsen.Server.CarsElections.Domain.Entities;
using Modsen.Server.CarsElections.Test.MockHelpers;
using Modsen.Server.Shared.Exceptions;
using Moq;

namespace Modsen.Server.CarsElections.Test.Features.Car.QueryHandlers
{
    public class GetCarCommentsHandlerUnitTest
    {
        [Fact]
        public async Task ReturnCarCommentsFromCache()
        {
            var car = new Domain.Entities.Car
            {
                Id = "1"
            };

            var loggerMock = new Mock<ILogger<GetCarComments>>().Object;
            var cacheRepositoryMock = CacheRepositoryMockHelper.MockCacheRepositoryGetEntity(car);
            var carRepositoryMock = CarRepositoryMockHelper.MockCarRepositoryGetCars([]);

            var getCarCommenstHandler = new GetCarCommentsHandler(
                carRepositoryMock,
                loggerMock,
                cacheRepositoryMock);

            var result = await getCarCommenstHandler.Handle(new GetCarComments
            {
                CarId = "1",
                Page = 1,
                Count = 10
            }, CancellationToken.None);

            Assert.Equal(car, result);
        }

        [Fact]
        public void ReturnNotFoundException()
        {
            var car = new Domain.Entities.Car
            {
                Id = "1"
            };

            var loggerMock = new Mock<ILogger<GetCarComments>>().Object;
            var cacheRepositoryMock = CacheRepositoryMockHelper.MockCacheRepositoryGetEntity(car);
            var carRepositoryMock = CarRepositoryMockHelper.MockCarRepositoryGetCars([]);

            var getCarCommenstHandler = new GetCarCommentsHandler(
                carRepositoryMock,
                loggerMock,
                cacheRepositoryMock);

            Task act()
            {
                return getCarCommenstHandler.Handle(new GetCarComments
                {
                    CarId = "2",
                    Page = 1,
                    Count = 10
                }, CancellationToken.None);
            }

            Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void ReturnCarComments()
        {
            var cars = new List<Domain.Entities.Car>
            {
                new () {
                    Id = "1"
                }
            };
            

            var loggerMock = new Mock<ILogger<GetCarComments>>().Object;
            var cacheRepositoryMock = CacheRepositoryMockHelper.MockCacheRepositoryGetEntity<Domain.Entities.Car>(null);
            var carRepositoryMock = CarRepositoryMockHelper.MockCarRepositoryGetCars(cars);

            var getCarCommenstHandler = new GetCarCommentsHandler(
                carRepositoryMock,
                loggerMock,
                cacheRepositoryMock);

            var result = await getCarCommenstHandler.Handle(new GetCarComments
            {
                CarId = "1",
                Page = 1,
                Count = 10
            }, CancellationToken.None);

            Assert.Equal("1", result.Id);
        }
    }
}
