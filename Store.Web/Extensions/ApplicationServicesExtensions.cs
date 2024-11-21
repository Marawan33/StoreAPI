﻿using Store.Repository.Interfaces;
using Store.Repository.UnitOfWork;
using Store.Service.Services.Products.Dtos;
using Store.Service.Services.Products;
using Store.Service.Services.CacheServices;

namespace Store.Web.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }
    }
}
