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
    internal class ProductTypeConfigrations : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.Property(pt => pt.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
