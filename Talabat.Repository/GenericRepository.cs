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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;
        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(ISpecification<T> Spec)
        {
            var output = await ApplySpecification(Spec).FirstOrDefaultAsync();
            return output;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> Spec)
        {
            return SpecificationsEvalutor<T>.GetQuery(_dbContext.Set<T>(), Spec);
        }
    }
}
