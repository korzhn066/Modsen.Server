using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Modsen.Server.CarsControl.Business.Models.Requests;
using Modsen.Server.CarsControl.Business.Services.Base;
using Modsen.Server.CarsControl.Business.Services;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Services;
using Modsen.Server.CarsControl.Test.Helpers;
using Modsen.Server.Shared.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsControl.Test.Services.Rent
{
    public class RentCarsServiceMoveCarUnitTest
    {
        [Fact]
        public void ReturnCarNotFound()
        {
            var mongoRepositoryMock = IMongoRepoitoryMockHelper.MockMongoReposioryReturnNullCar();
            var mongoRepositoryFactoryMock = IMongoRepositoryFactoryMockHelper.MockMongoRepositoryFactory(mongoRepositoryMock);
            var grpcServiceMock = new Mock<IGrpcService>().Object;
            var loggerCarRentMock = new Mock<ILogger<RentCarService>>().Object;
            var loggerCarBaseMock = new Mock<ILogger<CarServiceBase>>().Object;
            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>().Object;

            var rentCarService = new RentCarService(
                mongoRepositoryFactoryMock,
                grpcServiceMock,
                loggerCarBaseMock,
                loggerCarRentMock,
                webHostEnvironmentMock);

            Task act()
            {
                return rentCarService.MoveAsync(new MoveCar());
            }

            Assert.ThrowsAsync<NotFoundException>(act);
        }
    }
}
