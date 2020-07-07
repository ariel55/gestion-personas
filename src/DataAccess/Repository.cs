using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly GestionPersonaContext _context;
        protected DbSet<T> DbSet { get; }

        public Repository(GestionPersonaContext context)
        {
            this._context = context;
            DbSet = this._context.Set<T>();
        }
        public T Add(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            DbSet.Remove(entity);
            SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
            //DbSet.Update(entity);
            SaveChanges();
            return entity;
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public int SaveChanges()
        {
            return this._context.SaveChanges();
        }
    }
}