namespace Biblioteca.Presentation.Models
{
    public class EditarLivroViewModel
    {
        public Int32 Id { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public string Genero { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public string Sinopse { get; set; }
        public Guid Foto { get; set; }
        public IFormFile FotoJPGJPEG { get; set; }
        public Int32 UsuarioId { get; set; }
    }
}
