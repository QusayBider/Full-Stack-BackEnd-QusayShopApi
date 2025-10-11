using QusayShopApi.DAL.Models.Category;

namespace QusayShopApi.DAL.Repositories.Interfaces
{
    public interface ICategoryRepository: IGenericRepository<Category> 
    {
        bool checkedIfCategoryHasExist(string CategoryName);
    }
}