using Modsen.Server.CarsElections.Domain.Interfaces.Repositories.Base;
using Modsen.Server.CarsElections.Infrastructure.Data;

namespace Modsen.Server.CarsElections.Infrastructure.Repositories.Base
{
    public abstract class RepositoryBase<T>(DBContext context) : IRepositoryBase<T> where T : class
    {
        protected DBContext Context { get; set; } = context;

        public IQueryable<T> Query => Context.Set<T>();

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Context
                .Set<T>()
                .AddAsync(entity, cancellationToken);
        }

        public void Delete(T entity)
        {
            Context
                .Set<T>()
                .Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
