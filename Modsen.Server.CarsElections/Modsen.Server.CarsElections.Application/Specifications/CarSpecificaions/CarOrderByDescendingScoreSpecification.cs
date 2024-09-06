using Modsen.Server.CarsElections.Application.Specifications.Base;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Application.Specifications.CarSpecificaions
{
    public class CarOrderByDescendingScoreSpecification : SpecificationBase<Car>
    {
        public CarOrderByDescendingScoreSpecification()        {
            AddOrderByDescending(car => car.Comments.Sum(comment => comment.DislikeCount * -1 * (int)comment.Type) +
                    car.Comments.Sum(comment => comment.LikeCount * (int)comment.Type));
        }
    }
}
