using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.QueryHandlers;
using Modsen.Server.Authentication.Domain.Enums;
using Modsen.Server.Authentication.Test.MockHelpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Test.Features.ApplicationUser.QueryHandlers
{
    public class GetApplicationUsersHandlerUnitTest
    {
        [Fact]
        public async void GetUsers()
        {
            var user = new Domain.Entities.ApplicationUser()
            {
                UserName = "Test",
                Id = "",
                PhoneNumber = "1234567890",
                UserStatus = UserStatus.Ban
            };

            var users = new List<Domain.Entities.ApplicationUser>()
            {
                new()
                {
                    UserName = "Test1",
                },
                new()
                {
                    UserName = "Test1",
                },
                new()
                {
                    UserName = "Test1",
                }
            };

            var userManager = UserManagerMockHelper.MockUserManager(users);
            var loggerMock = new Mock<ILogger<GetApplicationUsersHandler>>().Object;
            var mapperMock = UserMapperMockHelper.MockUserMapper(user);

            var getApplicationUsersHandler = new GetApplicationUsersHandler(
                userManager, 
                loggerMock,
                mapperMock);

            var result = await getApplicationUsersHandler.Handle(new GetApplicationUsers
            {
                Page = 1,
                Count = 3
            }, CancellationToken.None);

            Assert.Equal(users.Count, result.Count);
        }
    }
}
