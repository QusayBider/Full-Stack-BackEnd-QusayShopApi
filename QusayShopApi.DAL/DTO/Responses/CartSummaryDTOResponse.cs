using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.DTO.Responses
{
    public class CartSummaryDTOResponse
    {
        public List<CartDTOResponse> Items { get; set; } = new List<CartDTOResponse>();
        public decimal TotalAmount => Items.Sum(i => i.TotalPrice);
        public int TotalItems => Items.Sum(i => i.Quantity);

    }
}
