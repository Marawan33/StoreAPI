using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Repository.Specification.ProductSpecs;
using Store.Service.Services.Products;
using Store.Service.Services.Products.Dtos;

namespace Store.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllBrands()
            => Ok(await _productService.GetAllBrandsAsync());

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllTypes()
            => Ok(await _productService.GetAllTypesAsync());

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts([FromQuery]ProductSpecification input)
            => Ok(await _productService.GetAllProductsAsync(input));

        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetProductById(int? id)
            => Ok(await _productService.GetByIdAsync(id));
    }
}
