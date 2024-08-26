using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsControl.DataAccess.Interfaces.Repositrory
{
    public interface IMongoRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(int page, int count, CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task AddAsync(T entity);
        Task UpdateAsync(string id, T entity);
        Task DeleteAsync(string id);
    }
}
