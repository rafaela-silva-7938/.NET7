using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Domain.Entities
{
    public class Cliente
    {
        #region Propriedades
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Cpf { get; set; }        
#endregion
#region Associações
public Endereco? Endereco { get; set; }
        public ICollection<Pedido>? Pedidos { get; set; }
        #endregion
    }
}