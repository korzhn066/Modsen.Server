using Modsen.Server.CarsElections.Application.Specifications.Base;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Application.Specifications.UserSpecifications
{
    internal class GetUserByUserNameSpecification(string userName) : SpecificationBase<User>(User => User.UserName == userName)
    {
    }
}
