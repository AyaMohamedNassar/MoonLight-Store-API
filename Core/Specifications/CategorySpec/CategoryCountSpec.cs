using Core.Entities;
using Core.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.CategorySpec
{
    public class CategoryCountSpec : BaseSpecification<Category>
    {
        public CategoryCountSpec(PaginationParams paginationParams)
            : base(category => string.IsNullOrEmpty(paginationParams.Search) || category.Name.ToLower().Contains(paginationParams.Search))
        {
        }
    }
}
