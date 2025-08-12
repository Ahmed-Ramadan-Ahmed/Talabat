using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Repository.Data
{
    public class StoreContext : DbContext 
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        // DbSet properties for your entities
        public DbSet<Talabat.Core.Entities.Product> Products { get; set; }
        public DbSet<Talabat.Core.Entities.ProductBrand> ProductBrands { get; set; }
        public DbSet<Talabat.Core.Entities.ProductType> ProductTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional model configuration can go here

        }
    }
}
