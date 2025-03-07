using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Biblioteca.Domain.Entities;

namespace Biblioteca.Infra.Data.Configurations
{
    public class LivroConfiguration : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            // Configuração da chave primária (PK)
            builder.HasKey(l => l.Id);

            // Configuração do autoincremento para o campo Id (no caso, Identity)
            builder.Property(l => l.Id)
                .ValueGeneratedOnAdd()  // Garante o autoincremento do campo Id
                .UseIdentityColumn();   // Configura o auto incremento no SQL Server

            // Relacionamento entre Livro e Usuario (FK)
            builder.HasOne(l => l.Usuario)                // Relacionamento com a entidade Usuario
                .WithMany(u => u.Livros)                  // Um usuário pode ter muitos livros
                .HasForeignKey(l => l.UsuarioId)          // Chave estrangeira no livro
                .OnDelete(DeleteBehavior.NoAction);       // Não deletar o livro ao deletar o usuário

            // Configuração do ISBN como único
            builder.HasIndex(l => l.ISBN).IsUnique();    // Garantir que o ISBN seja único
        }
    }
}
