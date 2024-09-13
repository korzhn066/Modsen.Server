using Microsoft.EntityFrameworkCore;
using Modsen.Server.CarsElections.Application.Specifications.Base;

namespace Modsen.Server.CarsElections.Application.Specifications
{
    public static class SpecificationQueryBuilder
    {
        public static IQueryable<T> GetQuery<T>(
            this IQueryable<T> inputQuery, SpecificationBase<T> specification) where T : class
        {
            var query = inputQuery;

            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.Includes.Count != 0)
            {
                query = specification.Includes
                    .Aggregate(query, (current, include) => current
                    .Include(include));
            }

            if (specification.OrderBy is not null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            return query;
        }
    }
}
