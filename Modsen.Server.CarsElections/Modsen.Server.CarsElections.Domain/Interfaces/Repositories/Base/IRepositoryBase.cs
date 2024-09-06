using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElections.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> Query {  get; }

        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        
        void Delete(T entity);
        
        Task SaveChangesAsync();
    }
}
