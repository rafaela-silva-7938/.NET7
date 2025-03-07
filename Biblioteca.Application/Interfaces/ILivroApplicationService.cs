using Biblioteca.Application.Commands;
using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Interfaces
{
    public interface ILivroApplicationService : IBaseApplicationService<Livro, Int32>
    {
        void Add(LivroCreateCommand command);
        void Update(LivroEditCommand command);
    }
}
