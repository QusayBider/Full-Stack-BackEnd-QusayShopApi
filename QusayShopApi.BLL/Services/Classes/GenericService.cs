using Mapster;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Responses;
using QusayShopApi.DAL.Models;
using QusayShopApi.DAL.Models.Category;
using QusayShopApi.DAL.Repositories.Classes;
using QusayShopApi.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Classes
{
    public class GenericService<TRequest, TResponse, TEntity>: IGenericService<TRequest, TResponse, TEntity> where TEntity : BaseModel {
       
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IGenericRepository<TEntity> GenericRepository)
        {
            _genericRepository = GenericRepository;
        }

        public int Create(TRequest request)
        {
            var entity = request.Adapt<TEntity>();
            return _genericRepository.Add(entity);
        }

        public int Delete(int id)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null) return 0;
            return _genericRepository.Remove(entity);
        }

        public IEnumerable<TResponse> GetAll(bool brands_InActive =false )
        {
            var entities = _genericRepository.GetAll();
            if (!brands_InActive) { 
                entities=_genericRepository.GetAll().Where(b=>b.Status == Status.Active);
            }
            return entities.Adapt<IEnumerable<TResponse>>();
        }

        public TResponse GetById(int id)
        {
            var entity = _genericRepository.GetById(id);
            return entity is null ? default : entity.Adapt<TResponse>();
        }

        public bool ToggleStatus(int id)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null) return false;
            entity.Status = entity.Status == Status.Active ? Status.In_Active : Status.Active;
            _genericRepository.Update(entity);
            return true;
        }

        public int Update(int id, TRequest request)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null) return 0;
            entity = request.Adapt(entity);
            return _genericRepository.Update(entity);
        }
    }
}
