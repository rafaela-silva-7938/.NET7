using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Domain.Entities
{
    public class ItemPedido
    {
        #region Propriedades
        public Guid? Id { get; set; }
        public Guid? CodigoItem { get; set; }
        public string? Nome { get; set; }
        public decimal? Preco { get; set; }
        public int? Quantidade { get; set; }
        public Guid? PedidoId { get; set; }
        #endregion
        #region Propriedades
        public Pedido? Pedido { get; set; }
        #endregion
    }
}