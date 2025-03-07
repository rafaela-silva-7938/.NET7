using Biblioteca.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Validations
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        public UsuarioValidation()
        {


            // Nome deve conter apenas letras e acentos, sem números ou caracteres especiais
            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .Length(6, 150).WithMessage("O nome deve ter entre 6 e 150 caracteres.")
                .Matches(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$").WithMessage("O nome deve conter apenas letras.");

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

            // Perfil deve ser 0 (usuário) ou 1 (administrador)
            RuleFor(u => u.Perfil)
                .InclusiveBetween(0, 1)
                .WithMessage("Perfil deve ser 0 (usuário) ou 1 (administrador).");

            // Data de nascimento válida e maior de 18 anos
            RuleFor(u => u.DataNascimento)
                .NotEmpty().WithMessage("Data de nascimento é obrigatória.")
                .Must(date => date <= DateTime.Now.AddYears(-18))
                .WithMessage("O usuário deve ter pelo menos 18 anos.");
        }
    }
}
