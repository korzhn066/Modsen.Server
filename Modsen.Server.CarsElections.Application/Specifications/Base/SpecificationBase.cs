using System.Linq.Expressions;

namespace Modsen.Server.CarsElections.Application.Specifications.Base
{
    public abstract class SpecificationBase<T> where T : class
    {
        protected SpecificationBase() { }

        protected SpecificationBase(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>>? Criteria { get; private set; }
        public List<Expression<Func<T, object>>> Includes { get; } = [];
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orederByExpression)
        {
            OrderBy = orederByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orederByExpression)
        {
            OrderByDescending = orederByExpression;
        }
    }
}
