using Core.Entities;
using Core.Interfaces;
using Core.Pagination;
using Core.Specifications;
using Core.Specifications.BrandSpec;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoonLight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("All")]
        public async Task<ActionResult<PaginatedList<Brand>>> GetAll(PaginationParams paginationParams)
        {
            BrandSpecification specification = new(paginationParams);

            IReadOnlyList<Brand> brands = await _unitOfWork.Repository<Brand>().ListAsync(specification);

            BrandCountSpec brandCountSpec = new BrandCountSpec(paginationParams);

            int totalItems = await _unitOfWork.Repository<Brand>().CountAsync(brandCountSpec);

            PaginatedList<Brand> paginatedBrands = new(brands,paginationParams.PageNumber,paginationParams.PageSize, totalItems);

            return Ok(paginatedBrands);
        }
    }
}
