using Microsoft.EntityFrameworkCore;
using Biblioteca.Domain.Entities;
using Biblioteca.Infra.Data.Configurations;

namespace Biblioteca.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new LivroConfiguration());
        }

        // DbSets para representar as tabelas no banco de dados
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
    }
}
