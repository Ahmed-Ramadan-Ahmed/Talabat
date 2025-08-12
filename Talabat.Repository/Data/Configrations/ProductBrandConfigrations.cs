using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Talabat.Repository.Data.Configrations
{
    internal class ProductBrandConfigrations : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.Property(pb => pb.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
