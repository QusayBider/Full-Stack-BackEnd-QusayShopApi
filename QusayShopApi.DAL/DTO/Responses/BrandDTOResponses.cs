using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.DTO.Responses
{
    public class BrandDTOResponses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string MainImage { get; set; }

        public string Image => $"https://localhost:7017/images/{MainImage}";
        public DateTime Create_at { get; set; } = DateTime.Now;
        public DateTime Update_at { get; set; }
        public string Status { get; set; } 
    }
}
