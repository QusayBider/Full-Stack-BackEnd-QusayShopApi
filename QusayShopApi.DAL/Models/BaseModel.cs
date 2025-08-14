using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Models
{
    public enum Status
    {
        In_Active,
        Active
    }
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime Create_at { get; set; } = DateTime.Now;
        public DateTime Update_at { get; set; }

        public Status status { get; set; } = Status.In_Active;
    }
}
