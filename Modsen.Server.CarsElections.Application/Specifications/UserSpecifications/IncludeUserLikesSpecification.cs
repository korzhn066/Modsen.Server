using Modsen.Server.CarsElections.Application.Specifications.Base;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Application.Specifications.UserSpecifications
{
    internal class IncludeUserLikesSpecification : SpecificationBase<User>
    {
        public IncludeUserLikesSpecification() 
        {
            AddInclude(User => User.Likes);
        }
    }
}
