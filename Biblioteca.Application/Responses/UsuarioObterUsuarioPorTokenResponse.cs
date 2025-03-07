using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Responses
{
    public class UsuarioObterUsuarioPorTokenResponse
    {
        public Int32 Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Token { get; set; }
        public string ResetSenhaToken { get; set; }  // Token para redefinição de senha
        public DateTime? ResetSenhaExpiraEm { get; set; }  // Data de expiração do token
        public string Perfil { get; set; }
    }

}
