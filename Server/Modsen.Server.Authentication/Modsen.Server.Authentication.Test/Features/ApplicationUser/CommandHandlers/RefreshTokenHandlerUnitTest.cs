using Microsoft.Extensions.Logging;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Models.Authentication;
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
    public class RefreshTokenHandlerUnitTest
    {
        [Fact]
        public void ReturnNotFoundException()
        {
            var userManagerMock = UserManagerMockHelper.MockUserManager<Domain.Entities.ApplicationUser>();
            var tokenProviderServiceMock = ITokenProviderServiceMockHelper.MockTokenProviderService("");
            var loggerMock = new Mock<ILogger<RefreshTokenHandler>>().Object;

            var refreshTokenHandler = new RefreshTokenHandler(
                tokenProviderServiceMock,
                userManagerMock,
                loggerMock);

            Task act()
            {
                return refreshTokenHandler.Handle(new RefreshToken
                {
                    RefreshTokenValidityInDays = 30,
                    OldRefreshToken = "asd"
                }, new CancellationToken());
            }

            Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public void ReturnBadRefreshToken() 
        {
            var user = new Domain.Entities.ApplicationUser
            {
                UserName = "test",
                RefreshToken = "test",
            };

            var userManagerMock = UserManagerMockHelper.MockUserManager(user);
            var tokenProviderServiceMock = ITokenProviderServiceMockHelper.MockTokenProviderService("");
            var loggerMock = new Mock<ILogger<RefreshTokenHandler>>().Object;

            var refreshTokenHandler = new RefreshTokenHandler(
                tokenProviderServiceMock,
                userManagerMock,
                loggerMock);

            Task act()
            {
                return refreshTokenHandler.Handle(new RefreshToken
                {
                    RefreshTokenValidityInDays = 30,
                    OldRefreshToken = "asd"
                }, new CancellationToken());
            }

            Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void ReturnRefreshToken()
        {
            var newRefreshToken = "newRefreshToken";
            var oldRefreshToken = "old";
            var user = new Domain.Entities.ApplicationUser
            {
                UserName = "test",
                RefreshToken = oldRefreshToken,
            };

            var userManagerMock = UserManagerMockHelper.MockUserManager(user);
            var tokenProviderServiceMock = ITokenProviderServiceMockHelper.MockTokenProviderService(newRefreshToken);
            var loggerMock = new Mock<ILogger<RefreshTokenHandler>>().Object;

            var refreshTokenHandler = new RefreshTokenHandler(
                tokenProviderServiceMock,
                userManagerMock,
                loggerMock);

            await refreshTokenHandler.Handle(new RefreshToken
            {
                RefreshTokenValidityInDays = 30,
                OldRefreshToken = oldRefreshToken
            }, new CancellationToken());

            Assert.Equal(newRefreshToken, user.RefreshToken);
        }
    }
}
