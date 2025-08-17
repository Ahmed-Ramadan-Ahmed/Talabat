using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationsEvalutor<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            if (specification.Criteria is not null)
            {
                inputQuery = inputQuery.Where(specification.Criteria);
            }
            foreach (var include in specification.Includes)
            {
                inputQuery = inputQuery.Include(include);
            }
            if (specification.OrderBy is not null)
            {
                inputQuery = inputQuery.OrderBy(specification.OrderBy);
            }
            if (specification.OrderByDescending is not null)
            {
                inputQuery = inputQuery.OrderByDescending(specification.OrderByDescending);
            }
            return inputQuery;
        }
    }
}
