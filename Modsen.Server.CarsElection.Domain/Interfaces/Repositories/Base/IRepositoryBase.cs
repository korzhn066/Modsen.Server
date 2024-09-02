using Modsen.Server.CarsElection.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElection.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> GetWithSpecification(ISpecification<T>? specification = null);
        Task SaveChangesAsync();
    }
}
