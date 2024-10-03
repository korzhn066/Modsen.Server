using Microsoft.Extensions.Logging;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Authentication.Domain.Enums;
using Modsen.Server.Authentication.Test.MockHelpers;
using Modsen.Server.Shared.Constants;
using Modsen.Server.Shared.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Test.Features.ApplicationUser.CommandHandlers
{
    public class LoginApplicationUserUnitTest
    {
        [Fact]
        public void ReturnNotFoundUser()
        {
            var userManagerMock = UserManagerMockHelper.MockUserManager<Domain.Entities.ApplicationUser>();
            var tokenProviderServiceMock = ITokenProviderServiceMockHelper.MockTokenProviderService("");
            var loggerMock = new Mock<ILogger<LoginApplicationUser>>().Object;

            var loginApplicationUserHandler = new LoginApplicationUserHandler(
                userManagerMock, 
                tokenProviderServiceMock,
                loggerMock);

            Task act()
            {
                return loginApplicationUserHandler.Handle(new LoginApplicationUser
                {
                    RefreshTokenValidityInDays = 30,
                    LoginModel = new LoginModel
                    {
                        Password = "test",
                        UserName = "test"
                    }
                }, new CancellationToken());
            }

            Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public void ReturnIncorrectPassword()
        {
            var user = new Domain.Entities.ApplicationUser
            {
                UserName = "test",
            };

            var userManagerMock = UserManagerMockHelper.MockUserManagerWithBadPassword(user);
            var tokenProviderServiceMock = ITokenProviderServiceMockHelper.MockTokenProviderService("");
            var loggerMock = new Mock<ILogger<LoginApplicationUser>>().Object;

            var loginApplicationUserHandler = new LoginApplicationUserHandler(
                userManagerMock,
                tokenProviderServiceMock,
                loggerMock);

            Task act()
            {
                return loginApplicationUserHandler.Handle(new LoginApplicationUser
                {
                    RefreshTokenValidityInDays = 30,
                    LoginModel = new LoginModel
                    {
                        Password = "test",
                        UserName = "test"
                    }
                }, new CancellationToken());
            }

            Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void ReturnUserWithRefreshToken() 
        {
            var newRefreshToken = "newToken";
            var user = new Domain.Entities.ApplicationUser
            {
                UserName = "test",
                RefreshToken = "test"
            };
            
            var userManagerMock = UserManagerMockHelper.MockUserManagerWithGoodPassword(user);
            var tokenProviderServiceMock = ITokenProviderServiceMockHelper.MockTokenProviderService(newRefreshToken);
            var loggerMock = new Mock<ILogger<LoginApplicationUser>>().Object;

            var loginApplicationUserHandler = new LoginApplicationUserHandler(
                userManagerMock,
                tokenProviderServiceMock,
                loggerMock);

            await loginApplicationUserHandler.Handle(new LoginApplicationUser
            {
                RefreshTokenValidityInDays = 30,
                LoginModel = new LoginModel
                {
                    Password = "test",
                    UserName = "test"
                }
            }, new CancellationToken());

            Assert.Equal(newRefreshToken, user.RefreshToken);
        }
    }
}
