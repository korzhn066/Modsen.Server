using Modsen.Server.CarsElections.Application.Specifications.Base;
using Modsen.Server.CarsElections.Domain.Entities;
using Modsen.Server.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElections.Application.Specifications.CarSpecificaions
{
    internal class CarTypeSpecification(CarType carType)
        : SpecificationBase<Car>(Car => Car.CarType == carType)
    {
    }
}
