using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Domain.Entities
{
    public class Cobranca
    {
        #region Propriedades
        public Guid? Id { get; set; }
        public string? NumeroCartao { get; set; }
        public string? NomeImpressoNoCartao { get; set; }
        public int? MesValidade { get; set; }
        public int? AnoValidade { get; set; }
        public int? CodigoSeguranca { get; set; }
        public decimal? ValorCobranca { get; set; }
        public Guid? PedidoId { get; set; }
        #endregion
        #region Associações
        public Pedido? Pedido { get; set; }
        #endregion
    }
}