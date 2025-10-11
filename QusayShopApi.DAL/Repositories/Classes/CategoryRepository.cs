using Microsoft.EntityFrameworkCore;
using QusayShopApi.DAL.Data;
using QusayShopApi.DAL.Models.Category;
using QusayShopApi.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Repositories.Classes
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public  bool checkedIfCategoryHasExist(string CategoryName) { 
        
            var category =  _context.Categories.FirstOrDefault(c => c.Name.ToLower() == CategoryName.ToLower());
            if (category is null) return false;
            return true;
        }
    }
}
