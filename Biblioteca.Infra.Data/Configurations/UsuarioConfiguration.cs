using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Biblioteca.Domain.Entities;

namespace Biblioteca.Infra.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Configuração da chave primária (PK)
            builder.HasKey(u => u.Id);

            // Configuração do autoincremento para o campo Id (no caso, Identity)
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd()  // Garante o autoincremento
                .UseIdentityColumn();   // Auto incremento no SQL Server

            // Relacionamento entre Usuario e Livro (1:N)
            builder.HasMany(u => u.Livros)                 // Um usuário pode ter muitos livros
                .WithOne(l => l.Usuario)                  // Um livro pertence a um único usuário
                .HasForeignKey(l => l.UsuarioId)          // Chave estrangeira no livro
                .OnDelete(DeleteBehavior.Cascade);        // Deletar os livros ao deletar o usuário
        }
    }
}
