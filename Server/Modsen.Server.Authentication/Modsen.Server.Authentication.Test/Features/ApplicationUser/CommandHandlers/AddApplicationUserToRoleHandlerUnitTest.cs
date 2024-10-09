using Microsoft.Extensions.Logging;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.CommandHandlers;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Queries;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.QueryHandlers;
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
    public class AddApplicationUserToRoleHandlerUnitTest
    {
        [Fact]
        public void ReturnNotFoundException()
        {
            var userManagerMock = UserManagerMockHelper.MockUserManager<Domain.Entities.ApplicationUser>();
            var loggerMock = new Mock<ILogger<AddApplicationUserToRoleByIdHandler>>().Object;

            var addApplicationUserToRoleByIdHandler = new AddApplicationUserToRoleByIdHandler(userManagerMock, loggerMock);

            Task act()
            {
                return addApplicationUserToRoleByIdHandler.Handle(new AddApplicationUserToRoleById
                {
                    UserId = "Test",
                    RoleName = "Test"
                }, new CancellationToken());
            }

            Assert.ThrowsAsync<NotFoundException>(act);
        }
    }
}
