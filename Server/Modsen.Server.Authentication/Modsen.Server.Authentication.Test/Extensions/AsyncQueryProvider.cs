using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Test.Extensions
{
    internal class AsyncQueryProvider<T>(IQueryProvider source) : IQueryProvider
    {
        private readonly IQueryProvider Source = source;

        public IQueryable CreateQuery(Expression expression) =>
            Source.CreateQuery(expression);

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) =>
            new AsyncQueryable<TElement>(Source.CreateQuery<TElement>(expression));

        public object Execute(Expression expression) => Execute<T>(expression);

        public TResult Execute<TResult>(Expression expression) =>
            Source.Execute<TResult>(expression);
    }
}
