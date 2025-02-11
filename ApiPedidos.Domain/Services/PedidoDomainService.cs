using ApiPedidos.Domain.Entities;
using ApiPedidos.Domain.Exceptions;
using ApiPedidos.Domain.Interfaces.Repositories;
using ApiPedidos.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Domain.Services
{
    public class PedidoDomainService : IPedidoDomainService
    {
        private readonly IUnitOfWork? _unitOfWork;
        public PedidoDomainService(IUnitOfWork? unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void RealizarPedido(Pedido pedido)
        {
            _unitOfWork?.BeginTransaction();
            try
            {
                #region Cadastrando o cliente e o endereço
                _unitOfWork?.ClienteRepository.Add(pedido.Cliente);
                pedido.Cliente.Endereco.ClienteId = pedido.Cliente.Id;
                _unitOfWork?.EnderecoRepository.Add(pedido.Cliente.Endereco);
                #endregion
                #region Cadastrando o pedido para o cliente
                pedido.ClienteId = pedido.Cliente.Id;
                _unitOfWork?.PedidoRepository.Add(pedido);
                #endregion
                #region Cadastrando os itens do pedido
                var total = 0m;
                foreach (var item in pedido.ItensPedido)
                {
                    item.PedidoId = pedido.Id;
                    _unitOfWork?.ItemPedidoRepository.Add(item);
                    total += (item.Preco.Value * item.Quantidade.Value);
                }
                if (total != pedido.Valor)
                    throw new PedidoInvalidoException(@"Valor do pedido é
                    inválido.Verifique os itens adicionados.");
                #endregion
                #region Cadastrando a cobrança do pedido
                if (pedido.Valor != pedido.Cobranca.ValorCobranca)
                    throw new PedidoInvalidoException(@"Valor da cobrança é
                    inválido.Verifique os dados do pedido.");
                pedido.Cobranca.PedidoId = pedido.Id;
                _unitOfWork?.CobrancaRepository.Add(pedido.Cobranca);
                #endregion
                _unitOfWork?.Commit();
            }
            catch (PedidoInvalidoException e)
            {
                _unitOfWork?.Rollback();
                throw new PedidoInvalidoException(e.Message);
            }

        }
        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}