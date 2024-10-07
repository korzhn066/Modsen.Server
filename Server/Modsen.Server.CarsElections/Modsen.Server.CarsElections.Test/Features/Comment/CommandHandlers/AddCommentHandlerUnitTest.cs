using AutoMapper;
using Microsoft.Extensions.Logging;
using Modsen.Server.CarsElections.Application.Features.Car.Queries;
using Modsen.Server.CarsElections.Application.Features.Car.QueryHandlers;
using Modsen.Server.CarsElections.Application.Features.Comment.CommandHandlers;
using Modsen.Server.CarsElections.Application.Features.Comment.Commands;
using Modsen.Server.CarsElections.Domain.Enums;
using Modsen.Server.CarsElections.Test.MockHelpers;
using Modsen.Server.Shared.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElections.Test.Features.Comment.CommandHandlers
{
    public class AddCommentHandlerUnitTest
    {
        [Fact]
        public void ReturnUserNotFoundException()
        {
            var car = new Domain.Entities.Car
            {
                Id = "1"
            };

            var loggerMock = new Mock<ILogger<AddCommentHandler>>().Object;
            var cacheRepositoryMock = CacheRepositoryMockHelper.MockCacheRepositoryGetEntity(car);
            var likeRepositoryMock = LikeRepositoryMockHelper.MockLikeRepositoryGetLikes([]);
            var userRepositoryMock = UserRepositoryMockHelper.MockUserRepositoryGetUsers([]);
            var mapperMock = new Mock<IMapper>().Object;

            var addCommentHandler = new AddCommentHandler(
                likeRepositoryMock,
                userRepositoryMock,
                loggerMock,
                cacheRepositoryMock,
                mapperMock);

            Task act()
            {
                return addCommentHandler.Handle(new AddComment
                {
                    UserName = "",
                    Message = "",
                    CommentType = CommentType.Positive,
                    CreatedAt = DateTime.UtcNow,
                    CarId = car.Id
                }, CancellationToken.None);
            }

            Assert.ThrowsAsync<NotFoundException>(act);
        }
    }
}
