using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        T Delete(T entity);
        T Update(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        int SaveChanges();
    }
}