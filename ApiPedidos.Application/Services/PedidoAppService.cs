using ApiPedidos.Application.Commands;
using ApiPedidos.Application.Events;
using ApiPedidos.Application.Interfaces;
using ApiPedidos.Domain.Entities;
using ApiPedidos.Domain.Interfaces.Services;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPedidos.Application.Services
{
    public class PedidoAppService : IPedidoAppService
    {
        private readonly IPedidoProducer? _pedidoProducer;
        private readonly IPedidoDomainService? _pedidoDomainService;
        private readonly IMapper _mapper;
        public PedidoAppService(IPedidoProducer? pedidoProducer,
        IPedidoDomainService? pedidoDomainService, IMapper mapper)
        {
            _pedidoProducer = pedidoProducer;
            _pedidoDomainService = pedidoDomainService;
            _mapper = mapper;
        }
        public async Task Add(PedidoCreateCommand command)
        {
            try
            {
                #region Realizar o cadastro do pedido
                var pedido = _mapper.Map<Pedido>(command);
                _pedidoDomainService?.RealizarPedido(pedido);
                #endregion
                #region Gerando um evento: Pedido realizado
                var @event = new PedidoRealizadoEvent
                {
                    EventId = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    DetalhesPedido = JsonConvert.SerializeObject(command)
                };
                await _pedidoProducer.Add(@event);
                #endregion
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}