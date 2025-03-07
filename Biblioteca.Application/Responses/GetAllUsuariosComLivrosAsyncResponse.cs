using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Responses
{
    public class GetAllUsuariosComLivrosAsyncResponse
    {
        public Int32 IdUsuario { get; set; }
        public string UsuarioNome { get; set; }
        public List<LivroResponse> LivrosResponse { get; set; }
    }
}
