using ApiPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Infra.Data.Configurations
{
    public class CobrancaConfiguration : IEntityTypeConfiguration<Cobranca>
    {
        public void Configure(EntityTypeBuilder<Cobranca> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.ValorCobranca)
            .HasColumnType("decimal(18,2)");
            builder.HasOne(c => c.Pedido)
            .WithOne(p => p.Cobranca)
            .HasForeignKey<Cobranca>(c => c.PedidoId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}