using Core.Entities;

namespace Core.Specifications.ProductSpec
{
    public class ProductCountSpec : BaseSpecification<Product>
    {
        public ProductCountSpec(ProductSpecParams productSpecParams)
        : base(product =>
               (string.IsNullOrEmpty(productSpecParams.Search) || product.Name.ToLower().Contains(productSpecParams.Search))
               && (!productSpecParams.BrandId.HasValue || product.BrandId == productSpecParams.BrandId)
               && (!productSpecParams.CategoryId.HasValue || product.CategoryId == productSpecParams.CategoryId))
        { }
    }
}
