using Azure;
using Azure.Core;
using Mapster;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.Data;
using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.DTO.Responses;
using QusayShopApi.DAL.Models;
using QusayShopApi.DAL.Models.Category;
using QusayShopApi.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Classes
{
    public class CategoryServices : GenericService<CategoryDTORequest, CategoryDTOResponses, Category>, ICategoryServices
    {
       
        public CategoryServices(ICategoryRepository Repository) : base(Repository)   {
        }

    }
}
