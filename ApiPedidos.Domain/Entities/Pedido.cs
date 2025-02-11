using ApiPedidos.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Domain.Entities
{
    public class Pedido
    {
        #region Propriedades
        public Guid? Id { get; set; }
        public DateTime? DataHora { get; set; }
        public decimal? Valor { get; set; }
        public StatusPedido Status { get; set; }
        public Guid? ProtocoloPagamento { get; set; }
        public string? DetalhesPagamento { get; set; }
        public Guid? ClienteId { get; set; }
        #endregion

        #region Associações
        public Cliente? Cliente { get; set; }
        public Cobranca? Cobranca { get; set; }
        public ICollection<ItemPedido>? ItensPedido { get; set; }
        #endregion
    }
}