using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
    public class ProductSpecParams
    {
        
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public int PageIndex { get; set; } = 1;

        private int pageSize = 10;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value <= 10) ? value : 10;
        }
        private string? search;
        public string? Search
        {
            get => search;
            set => search = value?.ToLower();
        }
        public ProductSpecParams()
        {
        }
        public ProductSpecParams(int pageIndex, int pageSize, string? sort, int? brandId, int? typeId)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Sort = sort;
            BrandId = brandId;
            TypeId = typeId;
        }
    }
}
