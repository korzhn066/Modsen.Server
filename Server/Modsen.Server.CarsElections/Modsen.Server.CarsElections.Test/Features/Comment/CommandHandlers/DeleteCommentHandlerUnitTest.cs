using Microsoft.Extensions.Logging;
using Modsen.Server.CarsElections.Application.Features.Comment.CommandHandlers;
using Modsen.Server.CarsElections.Application.Features.Comment.Commands;
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
    public class DeleteCommentHandlerUnitTest
    {
        [Fact]
        public void ReturnCommentNotFoundException()
        {
            var car = new Domain.Entities.Car
            {
                Id = "1"
            };

            var loggerMock = new Mock<ILogger<DeleteCommentHandler>>().Object;
            var commentRepository = CommentRepositoryMockHelper.MockCommentRepositoryGetComments([]);

            var deleteCommentHandler = new DeleteCommentHandler(
                commentRepository,
                loggerMock);

            Task act()
            {
                return deleteCommentHandler.Handle(new DeleteComment
                {
                    CommentId = 1,
                }, CancellationToken.None);
            }

            Assert.ThrowsAsync<NotFoundException>(act);
        }
    }
}
