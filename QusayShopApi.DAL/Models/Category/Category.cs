using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QusayShopApi.DAL.Models.Product;
namespace QusayShopApi.DAL.Models.Category
{
    public class Category:BaseModel
    {
        public string Name { get; set; }

        public List<Product.Product> Products { get; set; }= new List<Product.Product>();
    }
}
