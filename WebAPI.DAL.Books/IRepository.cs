using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAPI.DAL.Books
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> All { get; }
        TEntity Find(int key);
        void Insert(params TEntity[] obj);
        void Update(params TEntity[] obj);
        void Delete(params TEntity[] obj);
    }
}
