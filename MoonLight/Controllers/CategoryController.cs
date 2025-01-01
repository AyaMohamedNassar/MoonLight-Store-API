using Core.Entities;
using Core.Interfaces;
using Core.Pagination;
using Core.Specifications.CategorySpec;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoonLight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("All")]
        public async Task<ActionResult<PaginatedList<Category>>> GetAll(PaginationParams paginationParams)
        {
            CategorySpecification specification = new(paginationParams);

            IReadOnlyList<Category> categories = await _unitOfWork.Repository<Category>().ListAsync(specification);

            CategoryCountSpec categoryCountSpec = new(paginationParams);

            int totalItems = await _unitOfWork.Repository<Category>().CountAsync(categoryCountSpec);

            PaginatedList<Category> paginatedCategory = new(categories, paginationParams.PageNumber, paginationParams.PageSize,totalItems);

            return Ok(paginatedCategory);
            
        }
    }
}
