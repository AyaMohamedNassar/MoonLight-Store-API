using Core.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.ProductSpec
{
    public class ProductSpecParams : PaginationParams
    {
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
    }
}
