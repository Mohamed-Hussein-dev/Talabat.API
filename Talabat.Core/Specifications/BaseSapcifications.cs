using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSapcifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, bool>> Critertia { get; set ; }
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending {  get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool isPaginationEnable { get; set; }

        public BaseSapcifications() { }

        public BaseSapcifications(Expression<Func<T, bool>> critertia)
        {
            Critertia = critertia;
        }

        public void AddOrder(Expression<Func<T, object>> expression){ OrderBy = expression;}
        public void AddOrderDescending(Expression<Func<T, object>> expression) { OrderByDescending = expression; }
        
        public void ApplayPagination(int skip, int take)
        {
            isPaginationEnable = true;
            Skip = skip;
            Take = take;
        }
    }
}
