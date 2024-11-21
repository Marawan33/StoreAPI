using Store.Repository.Interfaces;
using Store.Repository.UnitOfWork;
using Store.Service.Services.Products.Dtos;
using Store.Service.Services.Products;
using Store.Service.Services.CacheServices;
using Microsoft.AspNetCore.Mvc;
using Store.Service.HandleResponse;

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


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var error = actionContext.ModelState
                        .Where(model => model.Value?.Errors.Count > 0)
                        .SelectMany(model => model.Value?.Errors)
                        .Select(error => error.ErrorMessage)
                        .ToList();

                    var errorResponse = new ValidationErrorResponse
                    {
                        Errors = error
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
