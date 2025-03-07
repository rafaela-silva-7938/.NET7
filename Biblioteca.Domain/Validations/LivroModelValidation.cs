using FluentValidation;
using System;
using System.IO;

namespace Biblioteca.Domain.Validations
{
    public class LivroModelValidation : AbstractValidator<(string Titulo, string ISBN, string Genero,
                                                 string Autor, string Editora, string Sinopse, string FotoJPGJPEG)>
    {
        public LivroModelValidation()
        {
            // Título não deve ser vazio e deve ter entre 3 e 200 caracteres
            RuleFor(l => l.Titulo)
                .NotEmpty().WithMessage("Título é obrigatório.")
                .Length(3, 200).WithMessage("O título deve ter entre 3 e 200 caracteres.");

            // ISBN deve ter 10 ou 13 dígitos numéricos
            RuleFor(l => l.ISBN)
                .NotEmpty().WithMessage("ISBN é obrigatório.")
                .Matches(@"^\d{10}(\d{3})?$").WithMessage("O ISBN deve ter 10 ou 13 dígitos numéricos.");

            // Gênero não deve aceitar números ou caracteres especiais, apenas letras e acentos
            RuleFor(l => l.Genero)
                .NotEmpty().WithMessage("Gênero é obrigatório.")
                .Matches(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$").WithMessage("O gênero deve conter apenas letras.")
                .Length(6, 150).WithMessage("O gênero deve ter entre 6 e 150 caracteres.");

            // Autor deve conter apenas letras e acentos
            RuleFor(l => l.Autor)
                .NotEmpty().WithMessage("Autor é obrigatório.")
                .Matches(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$").WithMessage("O nome do autor deve conter apenas letras.")
                .Length(4, 150).WithMessage("O autor deve ter entre 4 e 150 caracteres.");

            // Editora deve conter apenas letras e acentos
            RuleFor(l => l.Editora)
                .NotEmpty().WithMessage("Editora é obrigatória.")
                .Matches(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$").WithMessage("O nome da editora deve conter apenas letras.")
                .Length(2, 50).WithMessage("A editora deve ter entre 2 e 50 caracteres.");

            // Sinopse permite letras, números, espaços e alguns sinais de pontuação
            RuleFor(l => l.Sinopse)
                .NotEmpty().WithMessage("Sinopse é obrigatória.")
                .Length(10, 1000).WithMessage("A sinopse deve ter entre 10 e 1000 caracteres.")
                .Matches(@"^[A-Za-zÀ-ÖØ-öø-ÿ0-9\s.,:]+$").WithMessage("A sinopse contém caracteres inválidos.");


        }
    }
}
