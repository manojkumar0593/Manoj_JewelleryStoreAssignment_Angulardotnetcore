using JewelleryStore.DataAccess.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly JewelleryStoreDBContext _dbContext;
        public Repository(JewelleryStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public JewelleryStoreDBContext AppDataContext => _dbContext;

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public T Find(object id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public bool Delete(T entity)
        {
             var isDeleted = Convert.ToBoolean(_dbContext.Set<T>().Remove(entity));
            _dbContext.SaveChanges();
            return isDeleted;
        }

    }
}
