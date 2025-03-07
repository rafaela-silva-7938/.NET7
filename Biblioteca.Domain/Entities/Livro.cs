using Biblioteca.Domain.Validations;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Entities
{
    public class Livro
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

        //Associações
        public Usuario Usuario { get; set; }

        #region Validação dos dados do livro
        public ValidationResult Validar()
        {
            return new LivroValidation().Validate(this);
        }
        #endregion

    }
}
