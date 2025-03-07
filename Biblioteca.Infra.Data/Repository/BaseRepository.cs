using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces.Repositories;
using Biblioteca.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Repository
{
    public abstract class BaseRepository<TEntity, TKey>
        : IBaseRepository<TEntity, TKey>
        where TEntity : class
    {
        private readonly DataContext? _dataContext;
        protected BaseRepository(DataContext? dataContext)
        {
            _dataContext = dataContext;
        }

        public virtual void Add(TEntity entity)
        {
            _dataContext.Entry(entity).State = EntityState.Added;
            _dataContext.SaveChanges();
        }
        public virtual void Update(TEntity entity)
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }
        public virtual void Delete(TEntity entity)
        {
            _dataContext.Entry(entity).State = EntityState.Deleted;
            _dataContext.SaveChanges();
        }
        public virtual List<TEntity> GetAll()
        {
            return _dataContext.Set<TEntity>().ToList();
        }
        public virtual TEntity GetById(TKey id)
        {
            return _dataContext.Set<TEntity>().Find(id);
        }
        public virtual List<TEntity> GetAll(Func<TEntity, bool> where)
        {
            return _dataContext
            .Set<TEntity>()
            .Where(where)
            .ToList();
        }
        public virtual TEntity Get(Func<TEntity, bool> where)
        {
            return _dataContext
            .Set<TEntity>()
            .FirstOrDefault(where);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            if (_dataContext == null)
                throw new InvalidOperationException("DataContext está nulo.");

            return await _dataContext
                .Set<TEntity>()
                .FirstOrDefaultAsync(where);
        }

        public virtual void Dispose()
        {
            _dataContext.Dispose();
        }

    
    }
}
