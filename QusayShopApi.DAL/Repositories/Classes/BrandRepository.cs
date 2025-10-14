using Microsoft.EntityFrameworkCore;
using QusayShopApi.DAL.Data;
using QusayShopApi.DAL.Models.Brand;
using QusayShopApi.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Repositories.Classes
{
    public class BrandRepository: GenericRepository<Brand>, IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public bool checkedIfBrandHasExist(string BrandName)
        {
            var result = _context.Brands.FirstOrDefault(c => c.Name.ToLower() == BrandName.ToLower());
            if (result == null)
            {
                return false;
            }
            return true;
        }
        public async Task<List<Brand>> GetAllBrands()
        {
            return await _context.Brands.ToListAsync();
        }
        public async Task<Brand> GetBrandByIdAsync(int id)
        {
            return await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
