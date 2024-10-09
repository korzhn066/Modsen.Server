using AutoMapper;
using Elastic.CommonSchema;
using Modsen.Server.Authentication.Application.Models.Responses;
using Modsen.Server.Authentication.Domain.Entities;
using Modsen.Server.Authentication.Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Test.MockHelpers
{
    internal static class UserMapperMockHelper
    {
        public static IMapper MockUserMapper(ApplicationUser user)
        {
            var mockMapper = new Mock<IMapper>();

            var userResponse = new UserResponse
            {
                Id = user.Id,
                Role = "",
                UserName = user.UserName!,
                PhoneNumber = user.PhoneNumber!,
                UserStatus = user.UserStatus
            };

            var applicationUser = new ApplicationUser();

            mockMapper
                .Setup(mapper => mapper.Map<UserResponse>(It.IsAny<ApplicationUser>()))
                .Returns(userResponse);

            mockMapper
                .Setup(mapper => mapper.Map<UserResponse, ApplicationUser>(It.IsAny<UserResponse>()))
                .Returns(new ApplicationUser());

            return mockMapper.Object;
        }
    }
}
