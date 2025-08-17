using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications: BaseSpecifications<Product>
    {
        public ProductWithBrandAndTypeSpecifications(string? Sort, int? brandId, int? typeId) : 
            base( p => 
                    (!brandId.HasValue || brandId == p.ProductBrandId) 
                    &&
                    (!typeId.HasValue || typeId == p.ProductTypeId))
        {
            Includes.Add(x => x.ProductBrand);
            Includes.Add(x => x.ProductType);
            if(!string.IsNullOrEmpty(Sort))
            {
                switch(Sort.ToLower())
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
        public ProductWithBrandAndTypeSpecifications(int id) : base(x => x.Id == id)
        {
            Includes.Add(x => x.ProductBrand);
            Includes.Add(x => x.ProductType);
        } 
    }
}
