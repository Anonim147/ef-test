using System;
using System.Linq;
using System.Linq.Expressions;

using EFTasks.DAL.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EFTasks.DAL
{
    public class Repository<T> : IRepository<T> where T: TEntity
    {
        private readonly Context _context;

        public Repository(Context context)
        {
            _context = context;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null, bool isTracking = false)
        {
            IQueryable<T> entitites;
            if (!isTracking)
            {
                entitites = _context.Set<T>().AsNoTracking();
            }
            else 
            {
                entitites = _context.Set<T>();
            }
            if (filter != null)
                return entitites.Where(filter);

            return entitites;
        }

        public T Get(int Id, bool isTracking = false)
        {
            if (!isTracking)
            {
                return _context.Set<T>().AsNoTracking<T>().FirstOrDefault(task => task.Id == Id);
            }
            return _context.Set<T>().FirstOrDefault(task => task.Id == Id);
        }
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int Id) 
        {
            T entity = _context.Set<T>().Find(Id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

    }
}
