using AutoMapper;
using Elastic.CommonSchema;
using Microsoft.Extensions.Logging;
using Modsen.Server.CarsElections.Application.Features.Comment.CommandHandlers;
using Modsen.Server.CarsElections.Application.Features.Comment.Commands;
using Modsen.Server.CarsElections.Application.Features.Like.Command;
using Modsen.Server.CarsElections.Application.Features.Like.CommandHandlers;
using Modsen.Server.CarsElections.Domain.Entities;
using Modsen.Server.CarsElections.Domain.Enums;
using Modsen.Server.CarsElections.Test.MockHelpers;
using Modsen.Server.Shared.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElections.Test.Features.Like.CommandHandlers
{
    public class PutLikeHandlerUnitTest
    {
        [Fact]
        public void ReturnUserNotFoundException()
        {
            var loggerMock = new Mock<ILogger<PutLikeHandler>>().Object;
            var likeRepositoryMock = LikeRepositoryMockHelper.MockLikeRepositoryGetLikes([]);
            var userRepositoryMock = UserRepositoryMockHelper.MockUserRepositoryGetUsers([]);
            var commentRepositoryMock = CommentRepositoryMockHelper.MockCommentRepositoryGetComments([]);

            var putLikeHandler = new PutLikeHandler(
                likeRepositoryMock,
                commentRepositoryMock,
                userRepositoryMock,
                loggerMock);

            Task act()
            {
                return putLikeHandler.Handle(new PutLike
                {
                    LikeType = LikeType.Like,
                    CommentId = 1,
                    UserName = ""
                }, CancellationToken.None);
            }

            Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public void ReturnCommentNotFoundException()
        {
            var users = new List<Domain.Entities.User>
            {
                new()
                {
                    UserName = ""
                }
            };

            var loggerMock = new Mock<ILogger<PutLikeHandler>>().Object;
            var likeRepositoryMock = LikeRepositoryMockHelper.MockLikeRepositoryGetLikes([]);
            var userRepositoryMock = UserRepositoryMockHelper.MockUserRepositoryGetUsers(users);
            var commentRepositoryMock = CommentRepositoryMockHelper.MockCommentRepositoryGetComments([]);

            var putLikeHandler = new PutLikeHandler(
                likeRepositoryMock,
                commentRepositoryMock,
                userRepositoryMock,
                loggerMock);

            Task act()
            {
                return putLikeHandler.Handle(new PutLike
                {
                    LikeType = LikeType.Like,
                    CommentId = 1,
                    UserName = ""
                }, CancellationToken.None);
            }

            Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public void ReturnLikeNotFoundException()
        {
            var users = new List<Domain.Entities.User>
            {
                new()
                {
                    UserName = ""
                }
            };

            var comments = new List<Domain.Entities.Comment>
            {
                new()
                {
                    Id = 1
                }
            };

            var loggerMock = new Mock<ILogger<PutLikeHandler>>().Object;
            var likeRepositoryMock = LikeRepositoryMockHelper.MockLikeRepositoryGetLikes([]);
            var userRepositoryMock = UserRepositoryMockHelper.MockUserRepositoryGetUsers(users);
            var commentRepositoryMock = CommentRepositoryMockHelper.MockCommentRepositoryGetComments(comments);

            var putLikeHandler = new PutLikeHandler(
                likeRepositoryMock,
                commentRepositoryMock,
                userRepositoryMock,
                loggerMock);

            Task act()
            {
                return putLikeHandler.Handle(new PutLike
                {
                    LikeType = LikeType.Like,
                    CommentId = 1,
                    UserName = ""
                }, CancellationToken.None);
            }

            Assert.ThrowsAsync<NotFoundException>(act);
        }


        [Fact]
        public void ReturnLikeBadRequestException()
        {
            var users = new List<Domain.Entities.User>
            {
                new()
                {
                    UserName = "",
                    Id = "1"
                }
            };

            var comments = new List<Domain.Entities.Comment>
            {
                new()
                {
                    Id = 1
                }
            };

            var likes = new List<Domain.Entities.Like>
            {
                new()
                {
                    CommentId = 1,
                    User = users[0],
                    Type = LikeType.Like
                }
            };

            var loggerMock = new Mock<ILogger<PutLikeHandler>>().Object;
            var likeRepositoryMock = LikeRepositoryMockHelper.MockLikeRepositoryGetLikes(likes);
            var userRepositoryMock = UserRepositoryMockHelper.MockUserRepositoryGetUsers(users);
            var commentRepositoryMock = CommentRepositoryMockHelper.MockCommentRepositoryGetComments(comments);

            var putLikeHandler = new PutLikeHandler(
                likeRepositoryMock,
                commentRepositoryMock,
                userRepositoryMock,
                loggerMock);

            Task act()
            {
                return putLikeHandler.Handle(new PutLike
                {
                    LikeType = LikeType.Dislike,
                    CommentId = 1,
                    UserName = ""
                }, CancellationToken.None);
            }

            Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async Task ReturnChangeLike()
        {
            var users = new List<Domain.Entities.User>
            {
                new()
                {
                    UserName = "",
                    Id = "1"
                }
            };

            var comments = new List<Domain.Entities.Comment>
            {
                new()
                {
                    Id = 1
                }
            };

            var likes = new List<Domain.Entities.Like>
            {
                new()
                {
                    CommentId = 1,
                    User = users[0],
                    Type = LikeType.Like
                }
            };

            var loggerMock = new Mock<ILogger<PutLikeHandler>>().Object;
            var likeRepositoryMock = LikeRepositoryMockHelper.MockLikeRepositoryGetLikes(likes);
            var userRepositoryMock = UserRepositoryMockHelper.MockUserRepositoryGetUsers(users);
            var commentRepositoryMock = CommentRepositoryMockHelper.MockCommentRepositoryGetComments(comments);

            var putLikeHandler = new PutLikeHandler(
                likeRepositoryMock,
                commentRepositoryMock,
                userRepositoryMock,
                loggerMock);

            await putLikeHandler.Handle(new PutLike
            {
                LikeType = LikeType.Dislike,
                CommentId = 1,
                UserName = ""
            }, CancellationToken.None);

            Assert.Equal(LikeType.Dislike, likes[0].Type);
        }
    }
}
