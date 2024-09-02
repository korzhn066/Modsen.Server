using Modsen.Server.CarsElections.Application.Specifications.Base;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Application.Specifications.CarSpecificaions
{
    public class IncludeCarCommentsSpecification : SpecificationBase<Car>
    {
        public IncludeCarCommentsSpecification(int page, int count)
        {
            AddInclude(Car => Car.Comments.Skip(page * count).Take(count));
        }
    }
}
