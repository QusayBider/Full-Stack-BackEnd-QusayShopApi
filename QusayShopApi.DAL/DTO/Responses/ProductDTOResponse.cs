using QusayShopApi.DAL.Models;
using QusayShopApi.DAL.Models.Brand;
using QusayShopApi.DAL.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.DTO.Responses
{
    public class ProductDTOResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        [JsonIgnore]
        public string MainImage { get; set; }
        public DateTime Create_at { get; set; } = DateTime.Now;
        public DateTime Update_at { get; set; }
        public string Status { get; set; }
        public string Image => $"https://localhost:7017/images/{MainImage}";
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

    }
}
