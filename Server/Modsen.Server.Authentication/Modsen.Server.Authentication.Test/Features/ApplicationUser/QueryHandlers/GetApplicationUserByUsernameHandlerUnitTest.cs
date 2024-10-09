using AutoMapper;
using Microsoft.Extensions.Logging;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.QueryHandlers;
using Modsen.Server.Authentication.Domain.Enums;
using Modsen.Server.Authentication.Test.MockHelpers;
using Modsen.Server.Shared.Exceptions;
using Moq;

namespace Modsen.Server.Authentication.Test.Features.ApplicationUser.QueryHandlers
{
    public class GetApplicationUserByUsernameHandlerUnitTest
    {
        [Fact]
        public async void ReturnUser()
        {
            var user = new Domain.Entities.ApplicationUser() { 
                UserName = "Test",
                Id = "",
                PhoneNumber = "1234567890",
                UserStatus = UserStatus.Ban
            };

            var userManagerMock = UserManagerMockHelper.MockUserManager(user);
            var loggerMock = new Mock<ILogger<GetApplicationUserByUsernameHandler>>().Object;
            var mapperMock = UserMapperMockHelper.MockUserMapper(user);

            var getAppplicationUserByUsernameHandler = new GetApplicationUserByUsernameHandler(
                userManagerMock, 
                loggerMock,
                mapperMock);

            var result = await getAppplicationUserByUsernameHandler.Handle(new GetApplicationUserByUsername
            {
                Username = user.UserName
            }, CancellationToken.None);

            Assert.Equal(user.UserName, result.UserName);
        }

        [Fact]
        public async void ReturnNotFoundException()
        {
            var userManagerMock = UserManagerMockHelper.MockUserManager<Domain.Entities.ApplicationUser>();
            var loggerMock = new Mock<ILogger<GetApplicationUserByUsernameHandler>>().Object;
            var mapperMock = new Mock<IMapper>().Object;

            var getAppplicationUserByUsernameHandler = new GetApplicationUserByUsernameHandler(
                userManagerMock,
                loggerMock,
                mapperMock);

            Task act()
            {
                return getAppplicationUserByUsernameHandler.Handle(new GetApplicationUserByUsername
                {
                    Username = "NotFound"
                }, new CancellationToken());
            }

            await Assert.ThrowsAsync<NotFoundException>(act);
        }
    }
}
