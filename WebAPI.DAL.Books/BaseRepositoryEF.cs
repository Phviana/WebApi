using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAPI.DAL.Books
{
    public class BaseRepositoryEF<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ReadContext _context;

        public BaseRepositoryEF(ReadContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> All => _context.Set<TEntity>().AsQueryable();

        public void Update(params TEntity[] obj)
        {
            _context.Set<TEntity>().UpdateRange(obj);
            _context.SaveChanges();
        }

        public void Delete(params TEntity[] obj)
        {
            _context.Set<TEntity>().RemoveRange(obj);
            _context.SaveChanges();
        }

        public TEntity Find(int key)
        {
            return _context.Find<TEntity>(key);
        }

        public void Insert(params TEntity[] obj)
        {
            _context.Set<TEntity>().AddRange(obj);
            _context.SaveChanges();
        }
    }
}
