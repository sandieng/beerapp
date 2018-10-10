using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BeerRosterAPI.Entities
{
    public interface IBeerRosterRepository<T> where T : class
    {
        bool Save(T entity);
        IQueryable<T> GetAll();
        T GetById(int id);
        T GetByEmail(string email);
        T Single(Expression<Func<T, bool>> predicate = null,
                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                    bool disableTracking = true);
        void Update(T entity);
        void Delete(T entity);
    }
}