using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Interfaces
{
    public interface IGenericService<TRequest,TResponse,TEntity>
    {
        int Create(TRequest request);
        IEnumerable<TResponse> GetAll(bool brands_Active=false);
        int Delete(int id);
        int Update(int id, TRequest request);
        bool ToggleStatus(int id);
        TResponse GetById(int id);
    }
}
