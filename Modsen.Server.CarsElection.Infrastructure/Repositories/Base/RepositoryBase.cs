using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElection.Domain.Interfaces.Repositories.Base;
using Modsen.Server.CarsElection.Domain.Interfaces.Specification;
using Modsen.Server.CarsElection.Infrastructure.Data;
using System.Linq.Expressions;

namespace Modsen.Server.CarsElection.Infrastructure.Repositories.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DBContext Context { get; set; }
        public RepositoryBase(DBContext context)
        {
            Context = context;
        }

        public Task<IEnumerable<T>> GetWithSpecification(ISpecification<T>? specification = null)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
