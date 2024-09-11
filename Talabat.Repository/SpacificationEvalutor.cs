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
    public static class SpacificationEvalutor<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> dbset , ISpecifications<T> specifications)
        {
            IQueryable<T> query = dbset;

            if (specifications.Critertia is not null)
                query = query.Where(specifications.Critertia);


            if(specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);

            if(specifications.OrderByDescending is not null)
                query = query.OrderByDescending(specifications.OrderByDescending);

            if(specifications.isPaginationEnable)
                query = query.Skip(specifications.Skip).Take(specifications.Take);

            if (specifications.Includes.Count > 0)
                query = specifications.Includes.Aggregate(query, (curQuery, include) => curQuery.Include(include));

            return query;
        }
    }
}
