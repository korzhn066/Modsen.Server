using MockQueryable;
using MockQueryable.Moq;
using Modsen.Server.CarsElections.Domain.Entities;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElections.Test.MockHelpers
{
    internal static class CommentRepositoryMockHelper
    {
        public static ICommentRepository MockCommentRepositoryGetComments(List<Comment> comments)
        {
            var commentRepositoryMock = new Mock<ICommentRepository>();

            commentRepositoryMock
                .Setup(commentRepository => commentRepository.Query)
                .Returns(comments.BuildMock().BuildMockDbSet().Object);

            return commentRepositoryMock.Object;
        }
    }
}
