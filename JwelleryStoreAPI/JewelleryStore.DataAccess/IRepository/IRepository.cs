using JewelleryStore.DataAccess.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore.DataAccess
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);

        T Find(object id);

        IEnumerable<T> GetAll();

        bool Delete(T entity);
    }
}
