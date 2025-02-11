using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Application.Commands
{
    public class ClienteCreateCommand
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Cpf { get; set; }
        public EnderecoCreateCommand? Endereco { get; set; }
    }
}