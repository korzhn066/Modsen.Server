using Microsoft.Extensions.Logging;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
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
    public class ChangeApplicationUserRefreshTokenHandlerUnitTest
    {
        [Fact]
        public void ReturnNotFoundException()
        {
            var userManagerMock = UserManagerMockHelper.MockUserManager<Domain.Entities.ApplicationUser>();
            var loggerMock = new Mock<ILogger<ChangeApplicationUserRefreshTokenHandler>>().Object;

            var changeApplicationUserRefreshTokenHandler = new ChangeApplicationUserRefreshTokenHandler(userManagerMock, loggerMock);

            Task act()
            {
                return changeApplicationUserRefreshTokenHandler.Handle(new ChangeApplicationUserRefreshToken
                {
                    UserName = "Test",
                    RefreshToken = "Test",
                    RefreshTokenValidityInDays = 1
                }, new CancellationToken());
            }

            Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void ChangeRefreshToken()
        {
            var user = new Domain.Entities.ApplicationUser
            {
                UserName = "Test",
                RefreshToken = "Test",
                RefreshTokenExpiryTime = DateTime.UtcNow,
            };

            var userManagerMock = UserManagerMockHelper.MockUserManager(user);
            var loggerMock = new Mock<ILogger<ChangeApplicationUserRefreshTokenHandler>>().Object;

            var changeApplicationUserRefreshTokenHandler = new ChangeApplicationUserRefreshTokenHandler(userManagerMock, loggerMock);

            await changeApplicationUserRefreshTokenHandler.Handle(new ChangeApplicationUserRefreshToken
            {
                UserName = "Test",
                RefreshToken = "New",
                RefreshTokenValidityInDays = 1
            }, new CancellationToken());

            Assert.Equal("New", user.RefreshToken);
        }
    }
}
