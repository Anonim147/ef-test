using System.Threading.Tasks;
using EFTasks.DAL.Abstractions;

namespace EFTasks.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public Context _context;

        public UnitOfWork(Context context)
        {
            _context = context;
        }
        public IRepository<T> GetRepository<T>() where T : TEntity
        {
            return new Repository<T>(_context);
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
