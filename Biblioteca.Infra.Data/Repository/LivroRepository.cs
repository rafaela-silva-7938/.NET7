using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces.Repositories;
using Biblioteca.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Repository
{
    public class LivroRepository
    : BaseRepository<Livro, Int32>, ILivroRepository
    {
        private readonly DataContext? _dataContext;
        public LivroRepository(DataContext? dataContext)
        : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
