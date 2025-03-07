using Biblioteca.Application.Responses;

namespace Biblioteca.Application.Responses
{
    internal class UsuarioResponse 
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}