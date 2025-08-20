using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QusayShopApi.BLL.Services.Classes;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.Data;
using QusayShopApi.DAL.Models;
using QusayShopApi.DAL.Repositories.Classes;
using QusayShopApi.DAL.Repositories.Interfaces;
using QusayShopApi.DAL.Utils;
using Scalar;
using Scalar.AspNetCore;
using System.Threading.Tasks;
namespace QusayShopApi.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
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
            builder.Services.AddScoped<ISeedData, SeedData>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
                
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            var scope = app.Services.CreateScope();
            var ObjectOfseedData= scope.ServiceProvider.GetRequiredService<ISeedData>();
            await ObjectOfseedData.DataSeedingAsync();
            await ObjectOfseedData.IdentityRoleSeedingAsync();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
