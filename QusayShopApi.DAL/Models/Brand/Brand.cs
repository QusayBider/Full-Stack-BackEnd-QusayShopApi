using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QusayShopApi.DAL.Models.Brand
{
    public class Brand:BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string MainImage { get; set; }
        public List<Product.Product> Products { get; set; } = new List<Product.Product>();
    }
}
