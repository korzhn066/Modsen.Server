using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElection.Domain.Interfaces.Specification
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T item);
    }
}
