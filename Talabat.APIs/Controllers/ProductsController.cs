using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
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
        [ProducesResponseType(typeof(IEnumerable<ProductToReturnDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            ISpecification<Product> spec = new ProductWithBrandAndTypeSpecifications();
            var products = await _productRepo.GetAllAsync(spec);
            if (products is null || !products.Any())
            {
                return NotFound(new ApiResponse(404, "No products found"));
            }
            var MappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products);
            return Ok(MappedProducts);
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
        [ProducesResponseType(typeof(IEnumerable<ProductType>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetTypes()
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
        [ProducesResponseType(typeof(IEnumerable<ProductBrand>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
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
