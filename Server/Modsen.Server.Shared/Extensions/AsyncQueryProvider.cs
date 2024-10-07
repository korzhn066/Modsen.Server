using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Shared.Extensions
{
    internal class AsyncQueryProvider<T>(IQueryProvider source) : IQueryProvider
    {
        private readonly IQueryProvider _source = source;

        public IQueryable CreateQuery(Expression expression)
        {
            return _source.CreateQuery(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new AsyncQueryable<TElement>(_source.CreateQuery<TElement>(expression));
        }
            
        public object Execute(Expression expression)
        {
            return Execute<T>(expression);
        }

        public TResult Execute<TResult>(Expression expression) 
        { 
            return _source.Execute<TResult>(expression); 
        }
    }
}
