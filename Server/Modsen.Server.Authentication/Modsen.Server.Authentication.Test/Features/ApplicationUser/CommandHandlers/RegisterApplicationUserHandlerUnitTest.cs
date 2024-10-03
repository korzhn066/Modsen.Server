using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Models.Authentication;
using Modsen.Server.Authentication.Test.MockHelpers;
using Modsen.Server.Shared.Exceptions;
using Modsen.Server.Shared.Models.Kafka;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Test.Features.ApplicationUser.CommandHandlers
{
    public class RegisterApplicationUserHandlerUnitTest
    {
        [Fact]
        public void ReturnBadRequestException()
        {
            var userManagerMock = UserManagerMockHelper.MockUserManagerRegiterFailed<Domain.Entities.ApplicationUser>();
            var tokenProviderServiceMock = ITokenProviderServiceMockHelper.MockTokenProviderService("");
            var loggerMock = new Mock<ILogger<RegisterApplicationUserHandler>>().Object;
            var mapperMock = new Mock<IMapper>().Object;
            var topicProducerMock = new Mock<ITopicProducer<UserCreated>>().Object;

            var registerApplicationUserHandler = new RegisterApplicationUserHandler(
                userManagerMock,
                tokenProviderServiceMock,
                topicProducerMock,
                loggerMock,
                mapperMock);

            Task act()
            {
                return registerApplicationUserHandler.Handle(new RegisterApplicationUser
                {
                    RefreshTokenValidityInDays = 30,
                    RegisterModel = new RegisterModel
                    {
                        UserName = "Test",
                        Password = "test",
                        PhoneNumber = "1234567890"
                    }
                }, new CancellationToken());
            }

            Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public void ReturDbUpdateException()
        {
            var userManagerMock = UserManagerMockHelper.MockUserManagerAddToRoleFailed<Domain.Entities.ApplicationUser>();
            var tokenProviderServiceMock = ITokenProviderServiceMockHelper.MockTokenProviderService("");
            var loggerMock = new Mock<ILogger<RegisterApplicationUserHandler>>().Object;
            var mapperMock = new Mock<IMapper>().Object;
            var topicProducerMock = new Mock<ITopicProducer<UserCreated>>().Object;

            var registerApplicationUserHandler = new RegisterApplicationUserHandler(
                userManagerMock,
                tokenProviderServiceMock,
                topicProducerMock,
                loggerMock,
                mapperMock);

            Task act()
            {
                return registerApplicationUserHandler.Handle(new RegisterApplicationUser
                {
                    RefreshTokenValidityInDays = 30,
                    RegisterModel = new RegisterModel
                    {
                        UserName = "Test",
                        Password = "test",
                        PhoneNumber = "1234567890"
                    }
                }, new CancellationToken());
            }

            Assert.ThrowsAsync<DbUpdateException>(act);
        }
    }
}
