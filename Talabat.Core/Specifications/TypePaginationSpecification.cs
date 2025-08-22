using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class TypePaginationSpecification<T> : BaseSpecifications<T> where T : ProductType
    {
        public TypePaginationSpecification(BrandOrTypeSpecParams specParams) : 
            base(e => (specParams.Search == null) || ( e.Name.ToLower().Contains(specParams.Search.ToLower()) ))
        {
            if (specParams.SortByName && specParams.SortByName)
            {
                AddOrderBy(x => x.Name);
            }
            ApplyPaging((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
        }
    }
}