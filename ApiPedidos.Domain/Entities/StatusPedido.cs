using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Domain.Entities.Enums
{
    public enum StatusPedido
    {
        Em_Analise = 1,
        Pagamento_Aprovado = 2,
        Pagamento_Recusado = 3
    }
}