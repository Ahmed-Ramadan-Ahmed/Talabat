using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
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
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>) await _dbContext.Products.AsNoTracking()
                    .Include(e => e.ProductBrand)
                    .Include(e => e.ProductType)
                    .ToListAsync();
            }
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id) 
                ?? throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} not found.");
        }
    }
}
