using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Responses
{
    public class UsuarioValidarCredenciaisResponse
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }
        public Int32 Id { get; set; }
        public Int32 LivroId { get; set; }
    }
}
