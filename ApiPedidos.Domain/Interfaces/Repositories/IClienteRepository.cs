using ApiPedidos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Domain.Interfaces.Repositories
{
    public interface IClienteRepository
    : IBaseRepository<Cliente, Guid>
    {
    }
}