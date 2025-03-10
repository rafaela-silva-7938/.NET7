﻿// <auto-generated />
using System;
using ApiPedidos.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiPedidos.Infra.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250129185602_migration1")]
    partial class migration1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiPedidos.Domain.Entities.Cliente", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ApiPedidos.Domain.Entities.Cobranca", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("AnoValidade")
                        .HasColumnType("int");

                    b.Property<int?>("CodigoSeguranca")
                        .HasColumnType("int");

                    b.Property<int?>("MesValidade")
                        .HasColumnType("int");

                    b.Property<string>("NomeImpressoNoCartao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroCartao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PedidoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("ValorCobranca")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId")
                        .IsUnique()
                        .HasFilter("[PedidoId] IS NOT NULL");

                    b.ToTable("Cobrancas");
                });

            modelBuilder.Entity("ApiPedidos.Domain.Entities.Endereco", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cep")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Complemento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logradouro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId")
                        .IsUnique()
                        .HasFilter("[ClienteId] IS NOT NULL");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("ApiPedidos.Domain.Entities.ItemPedido", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CodigoItem")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PedidoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("Preco")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("ItensPedido");
                });

            modelBuilder.Entity("ApiPedidos.Domain.Entities.Pedido", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DataHora")
                        .HasColumnType("datetime2");

                    b.Property<string>("DetalhesPagamento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProtocoloPagamento")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal?>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("ApiPedidos.Domain.Entities.Cobranca", b =>
                {
                    b.HasOne("ApiPedidos.Domain.Entities.Pedido", "Pedido")
                        .WithOne("Cobranca")
                        .HasForeignKey("ApiPedidos.Domain.Entities.Cobranca", "PedidoId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("ApiPedidos.Domain.Entities.Endereco", b =>
                {
                    b.HasOne("ApiPedidos.Domain.Entities.Cliente", "Cliente")
                        .WithOne("Endereco")
                        .HasForeignKey("ApiPedidos.Domain.Entities.Endereco", "ClienteId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("ApiPedidos.Domain.Entities.ItemPedido", b =>
                {
                    b.HasOne("ApiPedidos.Domain.Entities.Pedido", "Pedido")
                        .WithMany("ItensPedido")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("ApiPedidos.Domain.Entities.Pedido", b =>
                {
                    b.HasOne("ApiPedidos.Domain.Entities.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("ApiPedidos.Domain.Entities.Cliente", b =>
                {
                    b.Navigation("Endereco");

                    b.Navigation("Pedidos");
                });

            modelBuilder.Entity("ApiPedidos.Domain.Entities.Pedido", b =>
                {
                    b.Navigation("Cobranca");

                    b.Navigation("ItensPedido");
                });
#pragma warning restore 612, 618
        }
    }
}
