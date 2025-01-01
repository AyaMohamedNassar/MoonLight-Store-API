using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Pagination;
using Core.Specifications.ProductSpec;
using Microsoft.AspNetCore.Mvc;
using MoonLight.API.DTOs;
using MoonLight.API.Errors;
using Swashbuckle.AspNetCore.Annotations;

namespace MoonLight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("All")]
        [SwaggerOperation(
            Summary = "Get a list of all products",
            Description = "Retrieves a list of all products with their associated categories and brands.")]
        [SwaggerResponse(200, "List of products retrieved successfully.", typeof(PaginatedList<ProductDTO>))]
        [SwaggerResponse(500, "Internal server error.")]
        public async Task<ActionResult<PaginatedList<ProductDTO>>> Products([FromQuery]ProductSpecParams productSpecParams)
        {
            var spec = new ProductWithCategoryBrandSpec(productSpecParams);

            IReadOnlyList<Product> products = await _unitOfWork.Repository<Product>().ListAsync(spec);

            var productDTOList = _mapper.Map<List<ProductDTO>>(products);

            var countSpec = new ProductCountSpec(productSpecParams);

            int totalItems = await _unitOfWork.Repository<Product>().CountAsync(countSpec);

            PaginatedList<ProductDTO> paginatedList =  
                new (productDTOList, productSpecParams.PageNumber, productSpecParams.PageSize, totalItems);

            return Ok(paginatedList);
        }
      
        [HttpGet("{Id}")]
        [SwaggerOperation(
            Summary = "Get a product by ID",
            Description = "Fetches a product with its associated category and brand details based on the provided ID."
        )]
        [SwaggerResponse(200, "Product retrieved successfully.", typeof(ProductDTO))]
        [SwaggerResponse(404, "Product not found.", typeof(ApiResponse))]
        [SwaggerResponse(500, "Internal server error.")]
        public async Task<ActionResult<Product>> GetProduct(int Id)
        {
            var spec = new ProductWithCategoryBrandSpec(Id);

            Product product = await _unitOfWork.Repository<Product>().GetEntity(spec);

            if (product == null) { return NotFound(new ApiResponse(404)); }

            return Ok(_mapper.Map<ProductDTO>( product));
        }
    }
}
