using ApiPedidos.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Application.Interfaces
{
    public interface IPedidoAppService
    {
        Task Add(PedidoCreateCommand command);
    }
}
