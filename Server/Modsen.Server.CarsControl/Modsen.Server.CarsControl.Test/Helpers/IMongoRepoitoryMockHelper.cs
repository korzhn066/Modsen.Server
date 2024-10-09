using Modsen.Server.CarsControl.DataAccess.Entities;
using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using Modsen.Server.CarsControl.DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsControl.Test.Helpers
{
    internal static class IMongoRepoitoryMockHelper
    {
        public static IMongoRepository MockMongoReposioryReturnNullCar()
        {
            var mockMongoRepository = new Mock<IMongoRepository>(); 

            return mockMongoRepository.Object;
        }

        public static IMongoRepository MockMongoReposioryReturnCar(CarDocument car)
        {
            var mockMongoRepository = new Mock<IMongoRepository>();

            mockMongoRepository
                .Setup(mongoRepoitory => mongoRepoitory.GetByIdAsync(It.IsAny<string>(), CancellationToken.None))
                .Returns(Task.FromResult(car)!);

            return mockMongoRepository.Object;
        }

        public static IMongoRepository MockMongoReposioryReturnCars(List<CarDocument> cars)
        {
            var mockMongoRepository = new Mock<IMongoRepository>();

            mockMongoRepository
                .Setup(mongoRepoitory => mongoRepoitory.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None))
                .Returns(Task.FromResult(cars)!);

            return mockMongoRepository.Object;
        }

        public static IMongoRepository MockMongoReposioryUpdateIsAcknowladgeError()
        {
            var mockMongoRepository = new Mock<IMongoRepository>();
            var mockUpdateResult = new Mock<UpdateResult>();
            
            mockUpdateResult
                .Setup(updateResult => updateResult.IsAcknowledged)
                .Returns(false);

            mockMongoRepository
                .Setup(mongoRepoitory => mongoRepoitory.UpdateAsync(It.IsAny<UpdateCar>()))
                .Returns(Task.FromResult(mockUpdateResult.Object));

            return mockMongoRepository.Object;
        }
    }
}
