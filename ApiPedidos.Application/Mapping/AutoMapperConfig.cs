using ApiPedidos.Application.Commands;
using ApiPedidos.Domain.Entities;
using ApiPedidos.Domain.Entities.Enums;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Application.Mappings
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {

            CreateMap<ClienteCreateCommand, Cliente>()
            .AfterMap((command, entity) =>
            {
                entity.Id = Guid.NewGuid();
            });
            CreateMap<EnderecoCreateCommand, Endereco>()
            .AfterMap((command, entity) =>
            {
                entity.Id = Guid.NewGuid();
            });
            CreateMap<CobrancaCreateCommand, Cobranca>()
            .AfterMap((command, entity) =>
            {
                entity.Id = Guid.NewGuid();
            });
            CreateMap<ItemPedidoCreateCommand, ItemPedido>()
            .AfterMap((command, entity) =>
            {
                entity.Id = Guid.NewGuid();
            });
            CreateMap<PedidoCreateCommand, Pedido>()
            .AfterMap((command, entity) =>
            {
                entity.Id = Guid.NewGuid();
                entity.Status = StatusPedido.Em_Analise;
            });

        }
    }
}