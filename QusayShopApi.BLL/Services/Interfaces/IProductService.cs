using Azure.Core;
using Microsoft.AspNetCore.Http;
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
        Task <string> AddProduct(ProductDTORequest request);
        Task<List<ProductDTOResponse>> GetAllProductsWithImages(HttpRequest request, int NumberOfPage = 1, int PageSize = 1, bool onlyActive = false);
        Task<ProductDTOResponse> GetProductById(int id);
        Task<string> DeleteProduct(int id);
        bool ToggleStatus(int id);
    }
}
