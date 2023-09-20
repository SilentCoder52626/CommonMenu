using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.BaseRepo
{
   public  interface BaseRepositoryInterface<T> where T : class
    {
        Task<IList<T>> GetAllAsync();
        IList<T> GetAll();
        void Insert(T entity);
        Task InsertAsync(T entity);
        Task InsertRangeAsync(IList<T> entities);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> GetQueryable();
        Task<T?> GetByIdAsync(long id);
        Task<T?> GetByIdAsync(int id);
        T? GetById(long id);
        T? GetById(int id);
    }
}
