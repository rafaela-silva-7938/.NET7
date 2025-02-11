using ApiPedidos.Domain.Interfaces.Repositories;
using ApiPedidos.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext? _dataContext;
        private IDbContextTransaction? _transaction;
        public UnitOfWork(DataContext? dataContext)
        {
            _dataContext = dataContext;
        }
        public void BeginTransaction()
        {
            _transaction = _dataContext.Database.BeginTransaction();
        }
        public void Commit()
        {
            _transaction.Commit();
        }
        public void Rollback()
        {
            _transaction.Rollback();
        }
        public IClienteRepository ClienteRepository
        => new ClienteRepository(_dataContext);
        public ICobrancaRepository CobrancaRepository
        => new CobrancaRepository(_dataContext);
        public IEnderecoRepository EnderecoRepository
        => new EnderecoRepository(_dataContext);
        public IItemPedidoRepository ItemPedidoRepository
        => new ItemPedidoRepository(_dataContext);
        public IPedidoRepository PedidoRepository
        => new PedidoRepository(_dataContext);

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}