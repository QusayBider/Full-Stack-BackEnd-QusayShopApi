using Azure.Core;
using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.DTO.Responses;
using QusayShopApi.DAL.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Interfaces
{
    public interface IProductService :IGenericService<ProductDTORequest,ProductDTOResponse,Product>
    {
        Task <int> CreateFile(ProductDTORequest request);
    }
}
