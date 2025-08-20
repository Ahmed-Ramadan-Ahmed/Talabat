using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BrandPaginationSpecification<T> : BaseSpecifications<T> where T : ProductBrand
    {
        public BrandPaginationSpecification(BrandOrTypeSpecParams specParams) : base()
        {
            if (specParams.SortByName && specParams.SortByName)
            {
                AddOrderBy(x => x.Name);
            }
            ApplyPaging((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
        }
    }
}