using Microsoft.EntityFrameworkCore;
using QusayShopApi.DAL.Data;
using QusayShopApi.DAL.Models;
using QusayShopApi.DAL.Models.Category;
using QusayShopApi.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Repositories.Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public int Add(T entity)
        {
            context.Set<T>().Add(entity);
            return context.SaveChanges();
        }

        public IEnumerable<T> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return context.Set<T>().ToList();

            return context.Set<T>().AsNoTracking().ToList();
        }

        public T? GetById(int id) => context.Set<T>().Find(id);


        public int Remove(T entity)
        {
            context.Set<T>().Remove(entity);
            return context.SaveChanges();
        }

        public int Update(T entity)
        {
            context.Set<T>().Update(entity);
            return context.SaveChanges();
        }
    }
}
