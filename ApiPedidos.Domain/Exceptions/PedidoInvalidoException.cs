using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPedidos.Domain.Exceptions
{
    /// <summary>
    /// Classe de exceçao customizada para retornar
    /// erros referentes a pedidos inválidos
    /// </summary>
    public class PedidoInvalidoException : Exception
    {
        public PedidoInvalidoException(string mensagem) : base(mensagem)
        {
        }
    }
}
