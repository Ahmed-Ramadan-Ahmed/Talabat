using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper ;
        public ProductsController(IGenericRepository<Product> ProductRepo, IMapper mapper)
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            ISpecification<Product> spec = new ProductWithBrandAndTypeSpecifications();
            var products = await _productRepo.GetAllAsync(spec);
            var MappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products);
            return Ok(MappedProducts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                ISpecification<Product> spec = new ProductWithBrandAndTypeSpecifications(id);
                var product = await _productRepo.GetByIdAsync(spec);
                var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(product);
                return Ok(MappedProduct);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
