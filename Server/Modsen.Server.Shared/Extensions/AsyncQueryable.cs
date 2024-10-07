using Microsoft.EntityFrameworkCore.Query;
using System.Collections;
using System.Linq.Expressions;

namespace Modsen.Server.Shared.Extensions
{
    internal class AsyncQueryable<T>(IQueryable<T> source) : IAsyncEnumerable<T>, IQueryable<T>
    {
        private readonly IQueryable<T> _source = source;

        public Type ElementType => typeof(T);

        public Expression Expression => _source.Expression;

        public IQueryProvider Provider => new AsyncQueryProvider<T>(_source.Provider);

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new AsyncEnumeratorWrapper<T>(_source.GetEnumerator());
        }

        public IEnumerator<T> GetEnumerator() => _source.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
