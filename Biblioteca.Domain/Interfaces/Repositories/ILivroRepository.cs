using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Interfaces.Repositories
{
    public interface ILivroRepository
    : IBaseRepository<Livro, Int32>
    {
    }
}
