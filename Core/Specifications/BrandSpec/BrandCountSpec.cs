using Core.Entities;
using Core.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.BrandSpec
{
    public class BrandCountSpec : BaseSpecification<Brand>
    {
        public BrandCountSpec(PaginationParams paginationParams)
            :base(brand => string.IsNullOrEmpty(paginationParams.Search) || brand.Name.ToLower().Contains(paginationParams.Search))
        {
            
        }
    }
}
