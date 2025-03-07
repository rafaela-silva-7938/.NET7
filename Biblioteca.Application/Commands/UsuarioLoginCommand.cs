using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Commands
{
    public class UsuarioLoginCommand
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Perfil { get; set; }
    }
}
