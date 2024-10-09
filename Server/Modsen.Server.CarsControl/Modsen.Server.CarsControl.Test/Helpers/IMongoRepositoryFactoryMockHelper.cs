using Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsControl.Test.Helpers
{
    internal static class IMongoRepositoryFactoryMockHelper
    {
        public static IMongoRepositoryFactory MockMongoRepositoryFactory(IMongoRepository mongoRepository)
        {
            var mockMongoRepositoryFactory = new Mock<IMongoRepositoryFactory>();

            mockMongoRepositoryFactory
                .Setup(mongoRepositoryFactory => mongoRepositoryFactory.Create(It.IsAny<string>()))
                .Returns(mongoRepository);

            return mockMongoRepositoryFactory.Object;
        }
    }
}
