using Modsen.Server.CarsElections.Application.Specifications.Base;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Application.Specifications.LikeSpecifications
{
    internal class LikeCommentIdSpecification(int commentId)
        : SpecificationBase<Like>(Like => Like.CommentId == commentId)
    {
    }
}
