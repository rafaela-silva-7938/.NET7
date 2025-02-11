using ApiPedidos.Domain.Entities;
using ApiPedidos.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ApiPedidos.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        // Construtor para injeção de dependência
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new EnderecoConfiguration());
            modelBuilder.ApplyConfiguration(new CobrancaConfiguration());
            modelBuilder.ApplyConfiguration(new ItemPedidoConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoConfiguration());
        }

        // DbSets para representar as tabelas no banco de dados
        public DbSet<Cliente>? Clientes { get; set; }
        public DbSet<Endereco>? Enderecos { get; set; }
        public DbSet<Cobranca>? Cobrancas { get; set; }
        public DbSet<ItemPedido>? ItensPedido { get; set; }
        public DbSet<Pedido>? Pedidos { get; set; }
    }
}
