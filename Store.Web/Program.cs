using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Repository.Interfaces;
using Store.Repository.UnitOfWork;
using Store.Service.Services.Products;
using Store.Service.Services.Products.Dtos;
using Store.Web.Helper;

namespace Store.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IProductService,ProductService>();
            builder.Services.AddAutoMapper(typeof(ProductProfile));
            builder.Services.AddDbContext<StoreDbContext>(options =>

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            await ApplySeeding.ApplySeedingAsync(app);

            app.MapControllers();

            app.Run();
            //ss
        }
    }
}