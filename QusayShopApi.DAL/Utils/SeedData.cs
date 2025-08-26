using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using QusayShopApi.DAL.Data;
using QusayShopApi.DAL.Models;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedData(ApplicationDbContext context,RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager) {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task DataSeedingAsync()
        {
            if (!(await _context.Database.GetAppliedMigrationsAsync()).Any()) { 
               await _context.Database.MigrateAsync();
            }
            if (!await _context.Categories.AnyAsync()) {
                await _context.Categories.AddRangeAsync(
                    new Models.Category.Category { Name = "Electronics" },
                    new Models.Category.Category { Name = "Clothing", },
                    new Models.Category.Category { Name = "Home Appliances"}
                );
            }

            if (!await _context.Brands.AnyAsync())
            {
                await _context.Brands.AddRangeAsync(
                    new Models.Brand.Brand { Name = "Samsung" },
                    new Models.Brand.Brand { Name = "Apple" },
                    new Models.Brand.Brand { Name = "LG" }
                );
            }

            await _context.SaveChangesAsync();

        }

        public async  Task IdentityRoleSeedingAsync()
        {
            if (!await _roleManager.Roles.AnyAsync()) {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            if (!await _userManager.Users.AnyAsync()) {
                var user1 = new ApplicationUser()
                {
                    Email = "QusayBider@gmail.com",
                    UserName = "QBdier",
                    FullName = "Qusay Bider",
                    PhoneNumber = "1234567890",
                    EmailConfirmed=true
                };
                var user2 = new ApplicationUser()
                {
                    Email = "AhmadBider@gmail.com",
                    UserName = "AhBdier",
                    FullName = "Ahmad Bider",
                    PhoneNumber = "1234567890",
                    EmailConfirmed = true

                };
                var user3 = new ApplicationUser()
                {
                    Email = "AliBider@gmail.com",
                    UserName = "ABdier",
                    FullName = "Ali Bider",
                    PhoneNumber = "1234567890",
                    EmailConfirmed = true

                };
                await _userManager.CreateAsync(user1, "Qusay@1234");
                await _userManager.CreateAsync(user2, "Ahmad@1234");    
                await _userManager.CreateAsync(user3, "Ali@1234");

                await _userManager.AddToRoleAsync(user1, "Admin");
                await _userManager.AddToRoleAsync(user2, "SuperAdmin");
                await _userManager.AddToRoleAsync(user3, "Customer");
            }

            await _context.SaveChangesAsync();
        }
    }
}
