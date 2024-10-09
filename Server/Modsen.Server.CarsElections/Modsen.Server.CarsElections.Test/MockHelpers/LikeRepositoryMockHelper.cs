using MockQueryable;
using MockQueryable.Moq;
using Modsen.Server.CarsElections.Domain.Entities;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElections.Test.MockHelpers
{
    internal static class LikeRepositoryMockHelper
    {
        public static ILikeRepository MockLikeRepositoryGetLikes(List<Like> likes)
        {
            var likeRepositoryMock = new Mock<ILikeRepository>();

            likeRepositoryMock
                .Setup(likeRepository => likeRepository.Query)
                .Returns(likes.BuildMock().BuildMockDbSet().Object);

            return likeRepositoryMock.Object;
        }
    }
}
