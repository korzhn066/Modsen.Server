using Modsen.Server.CarsElections.Application.Specifications.Base;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Application.Specifications.LikeSpecifications
{
    internal class LikeCarIdSpecification(string carId)
        : SpecificationBase<Like>(Like => Like.Comment.CarId == carId)
    {
    }
}
