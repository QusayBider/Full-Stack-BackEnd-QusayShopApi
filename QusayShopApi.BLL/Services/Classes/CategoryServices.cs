using Azure;
using Azure.Core;
using Mapster;
using Microsoft.Identity.Client;
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
        private readonly ICategoryRepository _categoryRepository;

        public CategoryServices(ICategoryRepository Repository,ICategoryRepository categoryRepository) : base(Repository)  {
            _categoryRepository = categoryRepository;
        }

        public bool checkedIfCategoryHasExist(string CategoryName)
        {
            var category = _categoryRepository.checkedIfCategoryHasExist(CategoryName);
            return category;
        }

    }
}
