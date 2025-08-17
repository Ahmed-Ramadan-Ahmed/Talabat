using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepo, IGenericRepository<ProductBrand> brandRepo, IGenericRepository<ProductType> typeRepo, IMapper mapper)
        {
            _productRepo = ProductRepo;
            _brandRepo = brandRepo;
            _typeRepo = typeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<ProductToReturnDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productsSpecParams)
        {
            ISpecification<Product> spec = new ProductWithBrandAndTypeSpecifications(productsSpecParams);
            var products = await _productRepo.GetAllAsync(spec);
            if (products is null || !products.Any())
            {
                return NotFound(new ApiResponse(404, "No products found"));
            }
            var MappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);


            var totalCount = await _productRepo.TotalCountAsync(new ProductWithFiltrationForCountAsync(productsSpecParams)); 
            var Result = new Pagination<ProductToReturnDto>()
            {
                Count = totalCount,
                PageIndex = productsSpecParams.PageIndex,
                PageSize = productsSpecParams.PageSize,
                Data = MappedProducts
            };
            return Ok(Result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            try
            {
                ISpecification<Product> spec = new ProductWithBrandAndTypeSpecifications(id);
                var product = await _productRepo.GetByIdAsync(spec);

                if (product is null)
                {
                    return NotFound(new ApiResponse(404));
                }

                var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(product);
                return Ok(MappedProduct);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }


        [HttpGet("Types")]
        [ProducesResponseType(typeof(IReadOnlyList<ProductType>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var spec = new BaseSpecifications<ProductType>();
            var types = await _typeRepo.GetAllAsync(spec);
            if (types is null || !types.Any())
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(types);
        }

        [HttpGet("Brands")]
        [ProducesResponseType(typeof(IReadOnlyList<ProductBrand>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var spec = new BaseSpecifications<ProductBrand>();
            var brands = await _brandRepo.GetAllAsync(spec);
            if (brands is null || !brands.Any())
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(brands);
        }
    }
}
