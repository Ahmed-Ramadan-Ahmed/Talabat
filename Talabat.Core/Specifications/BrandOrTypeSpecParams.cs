using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
    public class BrandOrTypeSpecParams
    {
        public bool sortByName { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; }
        public bool SortByName { get; set; }
        public BrandOrTypeSpecParams()
        {
            PageIndex = 1;
            PageSize = 10;
            SortByName = false;
        }
        public BrandOrTypeSpecParams(int? _pageIndex, int? _pageSize, bool? _sortByName)
        {
            PageIndex = _pageIndex.HasValue ? _pageIndex.Value : 1;
            PageSize = _pageSize.HasValue && _pageSize.Value <10 ? _pageSize.Value : 10;
            SortByName = _sortByName.HasValue ? _sortByName.Value : false;
        }
    }
}
