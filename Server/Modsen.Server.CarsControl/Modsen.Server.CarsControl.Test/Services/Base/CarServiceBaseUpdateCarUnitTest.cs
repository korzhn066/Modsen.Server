using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Modsen.Server.CarsControl.Business.Services.Base;
using Modsen.Server.CarsControl.Business.Services;
using Modsen.Server.CarsControl.DataAccess.Entities;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Services;
using Modsen.Server.CarsControl.Test.Helpers;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modsen.Server.CarsControl.DataAccess.Models;
using Modsen.Server.Shared.Exceptions;

namespace Modsen.Server.CarsControl.Test.Services.Base
{
    public class CarServiceBaseUpdateCarUnitTest
    {
        [Fact]
        public void ReturnNotFoundException()
        {
            var cars = new List<CarDocument>
            {
                new()
                {
                    Name = "1"
                },
                new()
                {
                    Name = "2"
                }
            };

            var mongoRepositoryMock = IMongoRepoitoryMockHelper.MockMongoReposioryReturnCars(cars);
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
                return electionsCarService.UpdateCarAsync(new UpdateCar());
            }

            Assert.ThrowsAsync<NotFoundException>(act);
        }
    }
}
