using QusayShopApi.DAL.Models.Category;

namespace QusayShopApi.DAL.Repositories
{
    public interface ICategoryRepository
    {
        Category GetById(int id);
        int Add(Category category);
        IEnumerable<Category> GetAll(bool withTracking=false);

        int Remove(Category category);
        int Update(Category category);

    }
}