using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Service.Services.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Products
{
    public class ProductService:IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
            var mappedBrands = brands.Select(x => new BrandTypeDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt
            }).ToList();
            return mappedBrands;
        }

        public async Task<IReadOnlyList<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Repository<Product, int>().GetAllAsync();
            var mappedProducts = products.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                BrandName = x.Brand.Name,
                TypeName = x.Type.Name,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                CreatedAt = x.CreatedAt
            }).ToList();
            return mappedProducts;
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            var mappedTypes = types.Select(x => new BrandTypeDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt
            }).ToList();
            return mappedTypes;
        }

        public async Task<ProductDto> GetByIdAsync(int? id)
        {
            if(id is null)
            {
                throw new Exception("Id Is Null");
            }
            var products = await _unitOfWork.Repository<Product, int>().GetByIdAsync(id.Value);
            if(products is null)
            {
                throw new Exception("Product Doesn't Exist");
            }
            var mappedProduct = new ProductDto
            {
                Id = products.Id,
                Name = products.Name,
                Description = products.Description,
                BrandName = products.Brand.Name,
                TypeName = products.Type.Name,
                ImageUrl = products.ImageUrl,
                Price = products.Price,
                CreatedAt = products.CreatedAt

            };
            return mappedProduct;
        }
    }
}
