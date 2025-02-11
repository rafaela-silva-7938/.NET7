using ApiPedidos.Domain.Entities;
using ApiPedidos.Domain.Interfaces.Repositories;
using ApiPedidos.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Infra.Data.Repositories
{
    public class ClienteRepository
    : BaseRepository<Cliente, Guid>, IClienteRepository
    {
        private readonly DataContext? _dataContext;
        public ClienteRepository(DataContext? dataContext)
        : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}