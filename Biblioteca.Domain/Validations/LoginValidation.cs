using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Validations
{
    public class LoginValidation : AbstractValidator<(string Email, string Senha)>
    {
        public LoginValidation()
        {
            // E-mail deve ser válido
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .Length(6, 50).WithMessage("O e-mail deve ter entre 6 e 50 caracteres.")
                .EmailAddress().WithMessage("Informe um email válido.");

            // Senha deve conter maiúscula, minúscula, número e caractere especial
            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("Senha é obrigatória.")
                .Matches(@"[A-Z]+").WithMessage("A senha deve conter pelo menos 1 letra maiúscula.")
                .Matches(@"[a-z]+").WithMessage("A senha deve conter pelo menos 1 letra minúscula.")
                .Matches(@"[0-9]+").WithMessage("A senha deve conter pelo menos 1 número.")
                .Matches(@"[\!\?\*\.\@]+").WithMessage("A senha deve conter pelo menos um caractere especial (!?*.@).")
                .Length(8, 50).WithMessage("A senha deve ter entre 8 e 50 caracteres.");

        }
    }
}