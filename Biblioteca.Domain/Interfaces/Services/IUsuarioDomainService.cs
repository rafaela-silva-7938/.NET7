using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Interfaces.Services
{
    public interface IUsuarioDomainService : IBaseDomainService<Usuario, Int32>
    {
        void CriarConta(Usuario usuario);
        Task<Usuario> ValidarCredenciaisAsync(string email, string senha);
        Task<Usuario> ObterUsuarioPorEmail(string email);
        Task<Usuario> ObterUsuarioPorToken(string token);
        Task<List<Usuario>> GetAllUsuariosComLivrosAsync();
    }
}
