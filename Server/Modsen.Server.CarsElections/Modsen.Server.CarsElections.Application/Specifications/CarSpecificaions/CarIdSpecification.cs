using Modsen.Server.CarsElections.Application.Specifications.Base;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Application.Specifications.CarSpecificaions
{
    internal class CarIdSpecification(string id)
        : SpecificationBase<Car>(Car => Car.Id == id)
    {
    }
}
