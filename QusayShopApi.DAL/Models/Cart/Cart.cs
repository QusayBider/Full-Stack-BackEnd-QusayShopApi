using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using QusayShopApi.DAL.Models.Product;
namespace QusayShopApi.DAL.Models.Cart
{
    [PrimaryKey(nameof(ProductId),nameof(UserId))]
    public class Cart
    {
        public int ProductId { get; set; }
        public Product.Product Product { get; set; }

        public string UserId {get;set;}

        public ApplicationUser User { get; set; }
        public int Quantity { get; set; }

    }
}
