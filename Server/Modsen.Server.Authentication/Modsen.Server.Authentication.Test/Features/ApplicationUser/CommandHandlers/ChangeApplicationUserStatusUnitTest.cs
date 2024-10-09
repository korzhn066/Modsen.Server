using Microsoft.Extensions.Logging;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Domain.Enums;
using Modsen.Server.Authentication.Test.MockHelpers;
using Modsen.Server.Shared.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Test.Features.ApplicationUser.CommandHandlers
{
    public class ChangeApplicationUserStatusUnitTest
    {
        [Fact]
        public async void ChangeUserStatus()
        {
            var user = new Domain.Entities.ApplicationUser
            {
                Id = "1",
                UserName = "Test",
                UserStatus = UserStatus.Ban
            };

            var userManagerMock = UserManagerMockHelper.MockUserManager(user);
            var loggerMock = new Mock<ILogger<ChangeApplicationUserStatusHandler>>().Object;

            var changeApplicationUserStatusHandler = new ChangeApplicationUserStatusHandler(userManagerMock, loggerMock);

            await changeApplicationUserStatusHandler.Handle(new ChangeApplicationUserStatus
            {
                UserId = "1",
                Status = UserStatus.Unban
            }, new CancellationToken());

            Assert.Equal(UserStatus.Unban, user.UserStatus);
        }

        [Fact]
        public void ReturnNotFoundException()
        {
            var userManagerMock = UserManagerMockHelper.MockUserManager<Domain.Entities.ApplicationUser>();
            var loggerMock = new Mock<ILogger<ChangeApplicationUserStatusHandler>>().Object;

            var changeApplicationUserStatusHandler = new ChangeApplicationUserStatusHandler(userManagerMock, loggerMock);

            Task act()
            {
                return changeApplicationUserStatusHandler.Handle(new ChangeApplicationUserStatus
                {
                    UserId = "",
                    Status = UserStatus.Ban
                }, new CancellationToken());
            }

            Assert.ThrowsAsync<NotFoundException>(act);
        }
    }
}
