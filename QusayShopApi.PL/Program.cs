using Microsoft.EntityFrameworkCore;
using QusayShopApi.BLL.Services.Classes;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.Data;
using QusayShopApi.DAL.Repositories.Classes;
using QusayShopApi.DAL.Repositories.Interfaces;
using Scalar;
using Scalar.AspNetCore;
namespace QusayShopApi.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnection"))) ;
            builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();

            builder.Services.AddScoped<ICategoryServices,CategoryServices>();
            builder.Services.AddScoped<IBrandServices, BrandServices>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
