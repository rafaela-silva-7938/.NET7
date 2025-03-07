using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Commands
{
    public class UsuarioUpdateCommand
    {
        public Int32 Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Perfil { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

    }
}
