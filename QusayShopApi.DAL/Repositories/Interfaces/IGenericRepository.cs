using QusayShopApi.DAL.Models;
using QusayShopApi.DAL.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        T? GetById(int id);
        int Add(T entity);
        IEnumerable<T> GetAll(bool withTracking = false);

        int Remove(T entity);
        int Update(T entity);
    }
}
