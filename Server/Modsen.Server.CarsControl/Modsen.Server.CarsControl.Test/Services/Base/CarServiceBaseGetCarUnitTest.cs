using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Modsen.Server.CarsControl.Business.Services.Base;
using Modsen.Server.CarsControl.Business.Services;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Services;
using Modsen.Server.CarsControl.Test.Helpers;
using Modsen.Server.Shared.Exceptions;
using Moq;
using Modsen.Server.CarsControl.DataAccess.Entities;
using MongoDB.Bson;

namespace Modsen.Server.CarsControl.Test.Services.Base
{
    public class CarServiceBaseGetCarUnitTest
    {
        [Fact]
        public void ReturnCarNotFoundException()
        {
            var mongoRepositoryMock = IMongoRepoitoryMockHelper.MockMongoReposioryReturnNullCar();
            var mongoRepositoryFactoryMock = IMongoRepositoryFactoryMockHelper.MockMongoRepositoryFactory(mongoRepositoryMock);
            var grpcServiceMock = new Mock<IGrpcService>().Object;
            var loggerCarElectionsMock = new Mock<ILogger<ElectionsCarService>>().Object;
            var loggerCarBaseMock = new Mock<ILogger<CarServiceBase>>().Object;
            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>().Object;

            var electionsCarService = new ElectionsCarService(
                mongoRepositoryFactoryMock,
                grpcServiceMock,
                loggerCarBaseMock,
                loggerCarElectionsMock,
                webHostEnvironmentMock);

            Task act()
            {
                return electionsCarService.GetCarAsync("", CancellationToken.None);
            }

            Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async Task ReturnNotFoundException()
        {
            var car = new CarDocument()
            {
                Name = "new"
            };

            var mongoRepositoryMock = IMongoRepoitoryMockHelper.MockMongoReposioryReturnCar(car);
            var mongoRepositoryFactoryMock = IMongoRepositoryFactoryMockHelper.MockMongoRepositoryFactory(mongoRepositoryMock);
            var grpcServiceMock = new Mock<IGrpcService>().Object;
            var loggerCarElectionsMock = new Mock<ILogger<ElectionsCarService>>().Object;
            var loggerCarBaseMock = new Mock<ILogger<CarServiceBase>>().Object;
            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>().Object;

            var electionsCarService = new ElectionsCarService(
                mongoRepositoryFactoryMock,
                grpcServiceMock,
                loggerCarBaseMock,
                loggerCarElectionsMock,
                webHostEnvironmentMock);

            var result = await electionsCarService.GetCarAsync("", CancellationToken.None);

            Assert.Equal(car.ToJson(), result);
        }
    }
}
