using System;
using System.Collections.Generic;
using System.Text;

namespace EFTasks.DAL.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : TEntity;

        int SaveChanges();
    }
}
