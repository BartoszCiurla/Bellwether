using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bellwether.Repositories.Context;
using Bellwether.Repositories.Factories;
using Microsoft.Data.Entity;

namespace Bellwether.Repositories.Repositories
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void InsertRange(IEnumerable<T> entities);
        void Delete(T entityToDelete);
        void Update(T entityToUpdate);
        T Get(Func<T, Boolean> where);
        void Delete(Func<T, Boolean> where);
        T GetSingle(Func<T, bool> predicate);
        T GetFirst(Func<T, bool> predicate);
        void Save();
    }

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly BellwetherDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository()
        {
            _context = RepositoryFactory.Context;
            _dbSet = _context.Set<TEntity>();
        }


        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public TEntity Get(Func<TEntity, bool> @where)
        {
            return _dbSet.FirstOrDefault(@where);
        }

        public void Delete(Func<TEntity, bool> @where)
        {
            IQueryable<TEntity> objects = _dbSet.Where(where).AsQueryable();
            foreach (TEntity obj in objects)
                _dbSet.Remove(obj);
        }

        public TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        public TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}
