using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces.Repositories;
using Biblioteca.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Repository
{
    public class UsuarioRepository
    : BaseRepository<Usuario, Int32>, IUsuarioRepository
    {
        private readonly DataContext? _dataContext;
        public UsuarioRepository(DataContext? dataContext)
        : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Usuario>> GetAllUsuariosComLivrosAsync()
        {
            return await _dataContext.Usuarios
                .Where(u => u.Livros.Any())
                .Include(u => u.Livros)
                .ToListAsync();
        }
    }
}
