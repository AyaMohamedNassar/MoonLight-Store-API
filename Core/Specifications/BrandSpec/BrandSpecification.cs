using Core.Entities;
using Core.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.BrandSpec
{
    public class BrandSpecification : BaseSpecification<Brand>
    {
        public BrandSpecification(PaginationParams paginationParams) 
            :base(brand => string.IsNullOrEmpty(paginationParams.Search) || brand.Name.ToLower().Contains(paginationParams.Search))
        {
            if (!string.IsNullOrEmpty(paginationParams.Sort))
            {
                switch (paginationParams.Sort) 
                {
                    case "NameASC":
                        AddOrderBy(brand => brand.Name);
                        break;
                    case "NameDSC":
                        AddOrderByDescending(brand => brand.Name);
                        break;
                    default:
                        break;
                }
            }

            ApplyPagination(paginationParams.PageSize * (paginationParams.PageNumber - 1), paginationParams.PageSize);
        }
    }
}
