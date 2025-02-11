using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Application.Commands
{
    public class PedidoCreateCommand
    {
        public DateTime? DataHora { get; set; }
        public decimal? Valor { get; set; }
        #region Associações
        public ClienteCreateCommand? Cliente { get; set; }
        public EnderecoCreateCommand? Endereco { get; set; }
        public CobrancaCreateCommand? Cobranca { get; set; }
        public List<ItemPedidoCreateCommand>? ItensPedido { get; set; }
        #endregion
    }
}