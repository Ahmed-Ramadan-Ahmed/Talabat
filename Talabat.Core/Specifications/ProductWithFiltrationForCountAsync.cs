using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithFiltrationForCountAsync: BaseSpecifications<Product>
    {
        public ProductWithFiltrationForCountAsync(ProductSpecParams productSpecParams) :
           base(p =>
                    (!productSpecParams.BrandId.HasValue || productSpecParams.BrandId == p.ProductBrandId)
                    &&
                    (!productSpecParams.TypeId.HasValue || productSpecParams.TypeId == p.ProductTypeId))
        {
            Includes.Add(x => x.ProductBrand);
            Includes.Add(x => x.ProductType);

            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort.ToLower())
                {
                    case "priceasc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(x => x.Price);
                        break;

                    case "type":
                        AddOrderBy(x => x.ProductType.Name);
                        break;

                    case "brand":
                        AddOrderBy(x => x.ProductBrand.Name);
                        break;

                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }
    }
}
