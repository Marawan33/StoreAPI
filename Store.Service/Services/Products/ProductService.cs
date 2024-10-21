using AutoMapper;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.ProductSpecs;
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
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
            var mappedBrands = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(brands);
            return mappedBrands;
        }

        //public async Task<IReadOnlyList<ProductDto>> GetAllProductsAsync()
        //{
        //    var products = await _unitOfWork.Repository<Product, int>().GetAllAsync();
        //    var mappedProducts = _mapper.Map<IReadOnlyList<ProductDto>>(products);
        //    return mappedProducts;
        //}
        public async Task<IReadOnlyList<ProductDto>> GetAllProductsAsync(ProductSpecification input)
        {
            var specs = new ProductWithSpecification(input);
            var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecificationAsync(specs);
            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            return mappedProducts;
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            var mappedTypes = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(types);
            return mappedTypes;
        }

        public async Task<ProductDto> GetByIdAsync(int? id)
        {
            if(id is null)
            {
                throw new Exception("Id Is Null");
            }
            var specs = new ProductWithSpecification(id);
            var products = await _unitOfWork.Repository<Product, int>().GetByIdWithSpeceficationAsync(specs);
            if(products is null)
            {
                throw new Exception("Product Doesn't Exist");
            }
            var mappedProduct = _mapper.Map<ProductDto>(products);
            return mappedProduct;
        }
    }
}
