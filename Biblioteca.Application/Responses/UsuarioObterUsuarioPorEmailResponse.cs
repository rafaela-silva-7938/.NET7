using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Responses
{
    public class UsuarioObterUsuarioPorEmailResponse
    {
        public Int32 Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        // Token para redefinição de senha
        public string? ResetSenhaToken { get; set; } // Token para redefinição de senha
        public DateTime? ResetSenhaExpiraEm { get; set; }
    }
}