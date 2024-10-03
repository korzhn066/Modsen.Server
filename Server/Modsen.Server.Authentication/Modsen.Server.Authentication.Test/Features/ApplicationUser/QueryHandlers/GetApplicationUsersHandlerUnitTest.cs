using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.QueryHandlers;
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

            var getApplicationUsersHandler = new GetApplicationUsersHandler(userManager, loggerMock);

            var result = await getApplicationUsersHandler.Handle(new GetApplicationUsers
            {
                Page = 1,
                Count = 3
            }, CancellationToken.None);

            Assert.Equal(users, result);
        }
    }
}
