using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Application.Commands
{
    public class ItemPedidoCreateCommand
    {
        public Guid? CodigoItem { get; set; }
        public string? Nome { get; set; }
        public decimal? Preco { get; set; }
        public int? Quantidade { get; set; }
    }
}