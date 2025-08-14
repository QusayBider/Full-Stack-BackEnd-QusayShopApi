using QusayShopApi.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QusayShopApi.DAL.Models.Category;

namespace QusayShopApi.DAL.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

    }
}
