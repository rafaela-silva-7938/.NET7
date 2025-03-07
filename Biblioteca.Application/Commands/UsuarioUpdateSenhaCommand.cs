using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Commands
{
    public class UsuarioUpdateSenhaCommand
    {
        public Int32 Id { get; set; }
        public string ResetSenhaToken { get; set; }
        public DateTime ResetSenhaExpiraEm { get; set; }
        public string NovaSenha { get; set; }
    }

}
