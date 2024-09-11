using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenaricRepository<T> : IGenarciRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _storeDbContext;

        public GenaricRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        #region get Without Specifications
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _storeDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _storeDbContext.Set<T>().FindAsync(id);
        }
        #endregion

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> specifications)
        {
            return await ApplaySpacification(specifications).ToListAsync();
        }

        

        public async Task<T> GetByIdWithSpecAsync(ISpecifications<T> specifications)
        {
            return await ApplaySpacification(specifications).FirstOrDefaultAsync();
        }


        public async Task<int> GetCountWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplaySpacification(spec).CountAsync();
        }

        private IQueryable<T> ApplaySpacification(ISpecifications<T> specifications)
        {
            return SpacificationEvalutor<T>.GetQuery(_storeDbContext.Set<T>() , specifications);
        }
    }
}
