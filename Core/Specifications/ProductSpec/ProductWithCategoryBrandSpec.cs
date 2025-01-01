using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.ProductSpec
{
    public class ProductWithCategoryBrandSpec : BaseSpecification<Product>
    {
        public ProductWithCategoryBrandSpec(ProductSpecParams productSpecParams)
        : base(
             product =>
             (string.IsNullOrEmpty(productSpecParams.Search) || product.Name.ToLower().Contains(productSpecParams.Search))
             && (!productSpecParams.BrandId.HasValue || product.BrandId == productSpecParams.BrandId)
             && (!productSpecParams.CategoryId.HasValue || product.CategoryId == productSpecParams.CategoryId))
        {
            AddInclude(product => product.Brand);
            AddInclude(product => product.Category);

            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
                {
                    case "PriceASC":
                        AddOrderBy(product => product.Price);
                        break;
                    case "PriceDES":
                        AddOrderByDescending(product => product.Price);
                        break;
                    default:
                        AddOrderBy(product => product.Name);
                        break;
                }
            }

            ApplyPagination(productSpecParams.PageSize * (productSpecParams.PageNumber - 1), productSpecParams.PageSize);
        }

        public ProductWithCategoryBrandSpec(int Id) : base(product => product.Id == Id)
        {
            AddInclude(product => product.Brand);
            AddInclude(product => product.Category);
        }
    }
}
