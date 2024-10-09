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
    internal static class UserRepositoryMockHelper
    {
        public static IUserRepository MockUserRepositoryGetUsers(List<User> users)
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock
                .Setup(userRepository => userRepository.Query)
                .Returns(users.BuildMock().BuildMockDbSet().Object);

            return userRepositoryMock.Object;
        }
    }
}
