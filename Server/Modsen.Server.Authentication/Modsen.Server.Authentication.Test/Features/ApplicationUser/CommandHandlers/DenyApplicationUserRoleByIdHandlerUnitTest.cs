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
    public class DenyApplicationUserRoleByIdHandlerUnitTest
    {
        [Fact]
        public void ReturnNotFoundException()
        {
            var userManagerMock = UserManagerMockHelper.MockUserManager<Domain.Entities.ApplicationUser>();
            var loggerMock = new Mock<ILogger<DenyApplicationUserRoleByIdHandler>>().Object;

            var denyApplicationUserRoleByIdHandler = new DenyApplicationUserRoleByIdHandler(userManagerMock, loggerMock);

            Task act()
            {
                return denyApplicationUserRoleByIdHandler.Handle(new DenyApplicationUserRoleById
                {
                    UserId = "1",
                    RoleName = "Admin"
                }, new CancellationToken());
            }

            Assert.ThrowsAsync<NotFoundException>(act);
        }
    }
}
