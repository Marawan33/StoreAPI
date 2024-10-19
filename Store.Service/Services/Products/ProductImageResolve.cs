using AutoMapper;
using AutoMapper.Execution;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using Store.Service.Services.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Products
{
    public class ProductImageResolve : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductImageResolve(IConfiguration configuration)
        {
            ;
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ImageUrl))
            {
                return $"{_configuration["BaseUrl"]}/{source.ImageUrl}";
            }
            return null ;
        }
    }
}
