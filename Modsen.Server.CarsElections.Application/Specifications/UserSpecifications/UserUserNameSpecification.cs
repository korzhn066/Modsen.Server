using Modsen.Server.CarsElections.Application.Specifications.Base;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Application.Specifications.UserSpecifications
{
    internal class UserUserNameSpecification : SpecificationBase<User>
    {
        public UserUserNameSpecification(string userName) : base(User => User.UserName == userName) { }
    }
}
