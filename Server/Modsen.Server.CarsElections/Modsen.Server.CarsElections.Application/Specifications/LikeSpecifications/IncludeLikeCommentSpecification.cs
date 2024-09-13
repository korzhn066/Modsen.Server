using Modsen.Server.CarsElections.Application.Specifications.Base;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Application.Specifications.LikeSpecifications
{
    internal class IncludeLikeCommentSpecification : SpecificationBase<Like>
    {
        public IncludeLikeCommentSpecification()
        {
            AddInclude(Like => Like.Comment);
        }
    }
}
