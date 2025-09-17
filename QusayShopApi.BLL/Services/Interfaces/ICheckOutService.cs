using Microsoft.AspNetCore.Http;
using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Interfaces
{
    public interface ICheckOutService
    {
        Task<CheckOutDTOResponse> ProcessPaymentAsync(CheckOutDTORequest request,string UserId, HttpRequest Request);
        Task<bool> HandelPaymentSuccessAsync(int OrderId);
    }
}
