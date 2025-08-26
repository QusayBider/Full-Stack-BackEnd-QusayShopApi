using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Models.Product
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public int Quantity { get; set; }

        public decimal? Discount { get; set; }
        public string MainImage { get; set; }
        public int? CategoryId { get; set; }
        public Category.Category Category { get; set; }

        public int? BrandId { get; set; }
        public Brand.Brand Brand { get; set; }
    }
}
