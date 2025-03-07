namespace Biblioteca.Presentation.Models
{
    public class UsuarioLivroBuscarTodosViewModel
    {
        public string UsuarioNome { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public string Genero { get; set; }
        public Int32 UsuarioId { get; set; }
        public Int32 LivroId { get; set; }
        public Guid Foto { get; set; }
        public string Sinopse { get; set; }
        public string CaminhoDaFoto { get; set; }
    }
}
