using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Modsen.Server.Authentication.Test.Extensions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Modsen.Server.Authentication.Test.MockHelpers
{
    internal class UserManagerMockHelper
    {
        public static UserManager<TUser> MockUserManager<TUser>(TUser user) where TUser : class
        {
            var mockStore = new Mock<IUserStore<TUser>>();
            var mockLookupNormalizer = new Mock<ILookupNormalizer>();

            mockLookupNormalizer
                .Setup(lookupNormalizer => lookupNormalizer.NormalizeName(It.IsAny<string>()))
                .Returns(It.IsAny<string>());

            mockStore
                .Setup(userManager => userManager.FindByNameAsync(It.IsAny<string>(), CancellationToken.None))
                .Returns(Task.FromResult(user)!);

            mockStore
                .Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>(), CancellationToken.None))
                .Returns(Task.FromResult(user)!);

            mockStore
                .Setup(userManager => userManager.UpdateAsync(It.IsAny<TUser>(), CancellationToken.None))
                .Returns(Task.FromResult(IdentityResult.Success));

            var userManager = new UserManager<TUser>(
                mockStore.Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<TUser>>().Object,
                [],
                [],
                mockLookupNormalizer.Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<TUser>>>().Object);

            return userManager;
        }

        public static UserManager<TUser> MockUserManagerWithBadPassword<TUser>(TUser user) where TUser : class
        {
            var userManager = new Mock<UserManager<TUser>>(
                new Mock<IUserStore<TUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<TUser>>().Object,
                Array.Empty<IUserValidator<TUser>>(),
                Array.Empty<IPasswordValidator<TUser>>(),
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<TUser>>>().Object);

            userManager
                .Setup(lookupNormalizer => lookupNormalizer.NormalizeName(It.IsAny<string>()))
                .Returns(It.IsAny<string>());

            userManager
                .Setup(userManager => userManager.CheckPasswordAsync(user, It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            userManager
                .Setup(userManager => userManager.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user)!);


            return userManager.Object;
        }

        public static UserManager<TUser> MockUserManagerWithGoodPassword<TUser>(TUser user) where TUser : class
        {
            var userManager = new Mock<UserManager<TUser>>(
                new Mock<IUserStore<TUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<TUser>>().Object,
                Array.Empty<IUserValidator<TUser>>(),
                Array.Empty<IPasswordValidator<TUser>>(),
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<TUser>>>().Object);

            userManager
                .Setup(lookupNormalizer => lookupNormalizer.NormalizeName(It.IsAny<string>()))
                .Returns(It.IsAny<string>());

            userManager
                .Setup(userManager => userManager.CheckPasswordAsync(user, It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            userManager
                .Setup(userManager => userManager.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user)!);


            return userManager.Object;
        }

        public static UserManager<TUser> MockUserManager<TUser>() where TUser : class
        {
            var userManager = new UserManager<TUser>(
                new Mock<IUserStore<TUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<TUser>>().Object,
                [],
                [],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<TUser>>>().Object);

            return userManager;
        }
        public static UserManager<TUser> MockUserManagerRegiterFailed<TUser>() where TUser : class
        {
            var userManager = new Mock<UserManager<TUser>>(
                new Mock<IUserStore<TUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<TUser>>().Object,
                Array.Empty<IUserValidator<TUser>>(),
                Array.Empty<IPasswordValidator<TUser>>(),
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<TUser>>>().Object);

            userManager
                .Setup(userManager => userManager.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Failed()));

            return userManager.Object;
        }

        public static UserManager<TUser> MockUserManagerAddToRoleFailed<TUser>() where TUser : class
        {
            var userManager = new Mock<UserManager<TUser>>(
                new Mock<IUserStore<TUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<TUser>>().Object,
                Array.Empty<IUserValidator<TUser>>(),
                Array.Empty<IPasswordValidator<TUser>>(),
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<TUser>>>().Object);

            userManager
                .Setup(userManager => userManager.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            userManager
                .Setup(userManager => userManager.AddToRoleAsync(It.IsAny<TUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Failed()));

            return userManager.Object;
        }

        public static UserManager<TUser> MockUserManager<TUser>(List<TUser> users) where TUser : class
        {
            var userManager = new Mock<UserManager<TUser>>(
                new Mock<IUserStore<TUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<TUser>>().Object,
                Array.Empty<IUserValidator<TUser>>(),
                Array.Empty<IPasswordValidator<TUser>>(),
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<TUser>>>().Object);

            userManager
                .Setup(userManager => userManager.Users)
                .Returns(users.AsAsyncQueryable());

            return userManager.Object;
        }
    }
}
