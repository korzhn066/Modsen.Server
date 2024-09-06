using Modsen.Server.CarsElections.Application.Specifications.Base;
using Modsen.Server.CarsElections.Domain.Entities;
using Modsen.Server.CarsElections.Domain.Enums;

namespace Modsen.Server.CarsElections.Application.Specifications.LikeSpecifications
{
    internal class LikeIsOwnerSpecification() : SpecificationBase<Like>(Like => Like.Type == LikeType.Owner)
    {
    }
}
