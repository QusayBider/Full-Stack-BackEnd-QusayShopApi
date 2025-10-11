using QusayShopApi.DAL.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Models.Review
{
    public class Review
    {
        public int Id { get; set; }
        public string? Comments { get; set; }
        public int Rate { get; set; }
        public int ProductId { get; set; }
        public QusayShopApi.DAL.Models.Product.Product Product { get; set; } 
        public ApplicationUser User { get; set; }
        public String UserId { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Ordering { get; set; }
    }
}
