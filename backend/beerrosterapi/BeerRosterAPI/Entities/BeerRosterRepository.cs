using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BeerRosterAPI.Entities
{
    public class BeerRosterRepository<T> : IBeerRosterRepository<T> where T : class
    {
        private BeerRosterContext _context;
        private DbSet<T> _entity;

        public BeerRosterRepository(BeerRosterContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public T Single(Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           bool disableTracking = true)
        {
            IQueryable<T> query = _entity;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return orderBy(query).FirstOrDefault();

            return query.FirstOrDefault();
        }

        public T GetByEmail(string email)
        {
            //return _entity.FirstOrDefaultAsync(x => x.Email == email);
            return null;
        }

        public  T GetById(int id)
        {
            _context.Members.AsNoTracking();
            return _entity.Find(id);
        }

        public bool Save(T entity)
        {
            _context.Add(entity);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return _entity.AsQueryable();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }
    }
}
