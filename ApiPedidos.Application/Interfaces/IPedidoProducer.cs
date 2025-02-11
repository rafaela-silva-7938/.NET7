using ApiPedidos.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPedidos.Application.Interfaces
{
    public interface IPedidoProducer
    {
        Task Add(PedidoRealizadoEvent @event);
    }
}
