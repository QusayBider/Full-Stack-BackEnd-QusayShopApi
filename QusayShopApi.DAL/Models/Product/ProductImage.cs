using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Models.Product
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }

        public int productId { get; set; }
        public Product product { get; set; }
    }
}
