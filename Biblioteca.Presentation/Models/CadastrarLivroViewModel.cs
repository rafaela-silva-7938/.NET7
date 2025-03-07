namespace Biblioteca.Presentation.Models
{
    public class CadastrarLivroViewModel
    {
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public string Genero { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public string Sinopse { get; set; }
        public IFormFile FotoJPGJPEG { get; set; }

    }
}
