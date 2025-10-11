using Microsoft.EntityFrameworkCore;
using QusayShopApi.DAL.Data;
using QusayShopApi.DAL.Models.Product;
using QusayShopApi.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Repositories.Classes
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public  List<Product> GetAllProductsWithImages() { 
        
            return   _context.Products.Include(p => p.SubImages).Include(b=>b.Brand).Include(c=>c.Category).Include(r=> r.Reviews).ThenInclude(R=>R.User).ToList();

        }
        public async Task<Product?> GetProductById(int productId) { 
            return await _context.Products.Include(p => p.SubImages).Include(r => r.Reviews).FirstOrDefaultAsync(p => p.Id == productId);
        }

    }
}
