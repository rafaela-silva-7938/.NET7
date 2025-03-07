using Biblioteca.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Entities
{
    public class Usuario
    {
        public Int32 Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Perfil { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }


        // Token para redefinição de senha
        public string? ResetSenhaToken { get; set; }
        public DateTime? ResetSenhaExpiraEm { get; set; }


        //Associações
        public List<Livro> Livros { get; set; }


        #region Validação dos dados do usuário
        public ValidationResult Validar()
        {
            return new UsuarioValidation().Validate(this);
        }
        #endregion

    }
}
