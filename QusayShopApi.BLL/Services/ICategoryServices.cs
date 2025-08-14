using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services
{
    public interface ICategoryServices
    {
        int CreateCategory(CategoryDTORequest request);
        IEnumerable<CategoryDTOResponses> GetAllCategories();
        int DeleteCategory(int id);
        int UpdateCategory(int id, CategoryDTORequest request);
        bool ToggleStatus(int id);
        CategoryDTOResponses GetCategoryById(int id);
    }
}
