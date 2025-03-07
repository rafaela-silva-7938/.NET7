using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity, TKey> : IDisposable
    where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        List<TEntity> GetAll();
        TEntity GetById(TKey id);
        List<TEntity> GetAll(Func<TEntity, bool> where);
        TEntity? Get(Func<TEntity, bool> where);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);
    }
}
