using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Validations
{
    public class RedefinirSenhaValidation : AbstractValidator<(string NovaSenha, string ConfirmarSenha)>
    {
        public RedefinirSenhaValidation()
        {
            RuleFor(u => u.NovaSenha)
                    .NotEmpty().WithMessage("Senha é obrigatória.")
                    .Matches(@"[A-Z]+").WithMessage("A senha deve conter pelo menos 1 letra maiúscula.")
                    .Matches(@"[a-z]+").WithMessage("A senha deve conter pelo menos 1 letra minúscula.")
                    .Matches(@"[0-9]+").WithMessage("A senha deve conter pelo menos 1 número.")
                    .Matches(@"[\!\?\*\.\@]+").WithMessage("A senha deve conter pelo menos um caractere especial (!?*.@).")
                    .Length(8, 50).WithMessage("A senha deve ter entre 8 e 50 caracteres.");

            RuleFor(u => u.NovaSenha)
                      .NotEmpty().WithMessage("A confirmação de senha é obrigatória.")
                      .Equal(u => u.ConfirmarSenha).WithMessage("As senhas não conferem.");

        }
    }
}