using Microsoft.AspNetCore.Http;
using QusayShopApi.DAL.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.DTO.Requests
{
    public class ProductDTORequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal? Discount { get; set; }
        public IFormFile MainImage { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
    }
}
