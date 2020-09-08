using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace EFTasks.DAL.Abstractions
{
    public interface IRepository<T>
        where T : class
    {
        IQueryable<T> Get(Expression<Func<T, bool>> filter = null, bool isTracking = false);
        T Get(int Id, bool isTracking = false);
        void Create(T entity);
        void Update(T entity);
        void Delete(int Id);
    }

}
