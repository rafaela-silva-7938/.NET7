using ApiPedidos.Domain.Interfaces.Repositories;
using ApiPedidos.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Infra.Data.Repositories
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
        public virtual void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}