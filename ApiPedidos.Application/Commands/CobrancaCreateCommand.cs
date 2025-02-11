using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Application.Commands
{
    public class CobrancaCreateCommand
    {
        public string? NumeroCartao { get; set; }
        public string? NomeImpressoNoCartao { get; set; }
        public int? MesValidade { get; set; }
        public int? AnoValidade { get; set; }
        public int? CodigoSeguranca { get; set; }
        public decimal? ValorCobranca { get; set; }
    }
}
