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
    public class CategorySpecification : BaseSpecification<Category>
    {
        public CategorySpecification(PaginationParams paginationParams) : 
            base(category => string.IsNullOrEmpty(paginationParams.Search) || category.Name.ToLower().Contains(paginationParams.Search))
        {
            if (!string.IsNullOrEmpty(paginationParams.Sort))
            {
                switch (paginationParams.Sort)
                {
                    case "NameASC":
                        AddOrderBy(categoty => categoty.Name);
                        break;
                    case "NameDSC":
                        AddOrderByDescending(category => category.Name);
                        break;
                    default: break;
                }
            }

            ApplyPagination(paginationParams.PageSize * (paginationParams.PageNumber - 1), paginationParams.PageSize);
        }
    }
}
