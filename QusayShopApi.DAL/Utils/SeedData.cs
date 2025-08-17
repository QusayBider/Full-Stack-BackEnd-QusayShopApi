using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using QusayShopApi.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Utils
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedData(ApplicationDbContext context,RoleManager<IdentityRole> roleManager) {
            _context = context;
            _roleManager = roleManager;
        }
        public void DataSeeding()
        {
            if (!_context.Database.GetAppliedMigrations().Any()) { 
                _context.Database.Migrate();
            }
            if (!_context.Categories.Any()) { 
                _context.Categories.AddRange(
                    new Models.Category.Category { Name = "Electronics" },
                    new Models.Category.Category { Name = "Clothing", },
                    new Models.Category.Category { Name = "Home Appliances"}
                );
            }

            if (!_context.Brands.Any())
            {
                _context.Brands.AddRange(
                    new Models.Brand.Brand { Name = "Samsung" },
                    new Models.Brand.Brand { Name = "Apple" },
                    new Models.Brand.Brand { Name = "LG" }
                );
            }

            _context.SaveChanges();

        }

        public void IdentityRoleSeeding()
        {
            // _roleManager.Crea
        }
    }
}
