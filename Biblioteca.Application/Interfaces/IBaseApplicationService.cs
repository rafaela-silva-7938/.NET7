using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Interfaces
{
    public interface IBaseApplicationService<TEntity, TKey> : IDisposable
    where TEntity : class
  { 
        void Delete(TKey id);
        List<TEntity> GetAll();
        TEntity GetById(TKey id);
    }
}
