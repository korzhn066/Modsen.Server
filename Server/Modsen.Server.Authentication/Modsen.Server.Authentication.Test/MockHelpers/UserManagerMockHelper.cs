﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Modsen.Server.Shared.Extensions;
using Moq;

namespace Modsen.Server.Authentication.Test.MockHelpers
{
    internal class UserManagerMockHelper
    {
        public static UserManager<TUser> MockUserManager<TUser>(TUser user) where TUser : class
        {
            var mockLookupNormalizer = new Mock<ILookupNormalizer>();

            mockLookupNormalizer
                .Setup(lookupNormalizer => lookupNormalizer.NormalizeName(It.IsAny<string>()))
                .Returns(It.IsAny<string>());

            var userManager = new Mock<UserManager<TUser>>(
                new Mock<IUserStore<TUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<TUser>>().Object,
                Array.Empty<IUserValidator<TUser>>(),
                Array.Empty<IPasswordValidator<TUser>>(),
                mockLookupNormalizer.Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<TUser>>>().Object);

            userManager
                .Setup(userManager => userManager.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user)!);

            userManager
                .Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user)!);

            userManager
                .Setup(userManager => userManager.UpdateAsync(It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            userManager
                .Setup(userManager => userManager.GetRolesAsync(It.IsAny<TUser>()))
                .Returns(Task.FromResult(new List<string>() as IList<string>)!);  

            return userManager.Object;
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

            userManager
                .Setup(userManager => userManager.GetRolesAsync(It.IsAny<TUser>()))
                .Returns(Task.FromResult(new List<string>() as IList<string>)!);

            return userManager.Object;
        }
    }
}
