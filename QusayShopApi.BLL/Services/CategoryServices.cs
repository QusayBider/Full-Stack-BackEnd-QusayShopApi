using Mapster;
using QusayShopApi.DAL.Data;
using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.DTO.Responses;
using QusayShopApi.DAL.Models;
using QusayShopApi.DAL.Models.Category;
using QusayShopApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryServices(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public CategoryDTOResponses GetCategoryById(int id)
        {
            var category = categoryRepository.GetById(id);
            return category is null ? null : category.Adapt<CategoryDTOResponses>();
        }

        public int CreateCategory(CategoryDTORequest request)
        {
            var category = request.Adapt<Category>();
            return categoryRepository.Add(category);
        }

        public int DeleteCategory(int id)
        {
            var category = categoryRepository.GetById(id);
            if (category is null) return 0;
            return categoryRepository.Remove(category);
        }

        public IEnumerable<CategoryDTOResponses> GetAllCategories()
        {
            var categories = categoryRepository.GetAll();
            return categories.Adapt<IEnumerable<CategoryDTOResponses>>();
        }

        public int UpdateCategory(int id, CategoryDTORequest request)
        {
            var category = categoryRepository.GetById(id);
            if (category is null) return 0;
            category = request.Adapt(category);
            return categoryRepository.Update(category);
        }
        public bool ToggleStatus(int id)
        {
            var category = categoryRepository.GetById(id);
            if (category is null) return false;
            category.status = category.status == Status.Active ? Status.In_Active : Status.Active;
            categoryRepository.Update(category);
            return true;
        }
    }
}
