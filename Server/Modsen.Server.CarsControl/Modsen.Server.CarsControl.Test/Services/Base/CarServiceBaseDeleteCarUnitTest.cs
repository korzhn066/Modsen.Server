using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Modsen.Server.CarsControl.Business.Services.Base;
using Modsen.Server.CarsControl.Business.Services;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Services;
using Modsen.Server.CarsControl.Test.Helpers;
using Modsen.Server.Shared.Exceptions;
using Moq;

namespace Modsen.Server.CarsControl.Test.Services.Base
{
    public class CarServiceBaseDeleteCarUnitTest
    {
        [Fact]
        public void ReturnUserNotFoundException()
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
                return electionsCarService.DeleteCarAsync("");
            }

            Assert.ThrowsAsync<NotFoundException>(act);
        }
    }
}
