using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        IClienteRepository ClienteRepository { get; }
        ICobrancaRepository CobrancaRepository { get; }
        IEnderecoRepository EnderecoRepository { get; }
        IItemPedidoRepository ItemPedidoRepository { get; }
        IPedidoRepository PedidoRepository { get; }
    }
}