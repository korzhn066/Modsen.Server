using Modsen.Server.CarsElections.Application.Specifications.Base;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Application.Specifications.LikeSpecifications
{
    internal class LikeUsernameSpecification(string userName)
        : SpecificationBase<Like>(Like => Like.User.UserName == userName)
    {
    }
}
