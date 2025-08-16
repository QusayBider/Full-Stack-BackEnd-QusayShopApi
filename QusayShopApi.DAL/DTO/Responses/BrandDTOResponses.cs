using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.DTO.Responses
{
    public class BrandDTOResponses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Create_at { get; set; } = DateTime.Now;
        public DateTime Update_at { get; set; }
        public string Status { get; set; } = "In_Active"; 
    }
}
