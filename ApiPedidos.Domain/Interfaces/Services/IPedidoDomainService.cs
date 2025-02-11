using ApiPedidos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Domain.Interfaces.Services
{    public interface IPedidoDomainService : IDisposable
    {
        void RealizarPedido(Pedido pedido);
    }
}