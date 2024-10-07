using Modsen.Server.CarsElections.Domain.Entities;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;
using Moq;

namespace Modsen.Server.CarsElections.Test.MockHelpers
{
    internal static class CacheRepositoryMockHelper
    {
        public static ICacheRepository MockCacheRepositoryGetEntity<Entity>(Entity? entity)
        {
            var mockCacheRepository = new Mock<ICacheRepository>();

            mockCacheRepository
                .Setup(cacheRepository => cacheRepository.GetAsync<Entity?>(It.IsAny<string>()))
                .Returns(Task.FromResult(entity)!);

            return mockCacheRepository.Object;
        }
    }
}
