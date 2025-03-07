using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Commands
{
    public class LivroEditCommand
    {
        public Int32 Id { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public string Genero { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public string Sinopse { get; set; }
        public Guid Foto { get; set; }
        public Int32 UsuarioId { get; set; }
    }
}
