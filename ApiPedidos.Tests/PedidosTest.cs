using ApiPedidos.Application.Commands;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Xunit;


namespace teste
{
    public class PedidosTest
    {
        [Fact]
        public async Task Test_Post_Pedidos_Returns_Created()
        {
            var pedidoFaker = new PedidoFaker();
            var pedido = pedidoFaker.Generate();
            var content = new StringContent
            (JsonConvert.SerializeObject(pedido),
            Encoding.UTF8, "application/json");
            var httpClient = new WebApplicationFactory<Program>().CreateClient();
            var result = await httpClient.PostAsync("api/pedidos", content);
            result.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }


    public class PedidoFaker : Faker<PedidoCreateCommand>
    {
        public PedidoFaker()
        {
            RuleFor(p => p.DataHora, f => f.Date.Future());
            RuleFor(p => p.Valor, f => f.Random.Decimal(1, 10000));
            RuleFor(p => p.Cliente, f => new ClienteFaker().Generate());
            RuleFor(p => p.Cobranca, f => new CobrancaFaker().Generate());
            RuleFor(p => p.ItensPedido, f =>
            {
                var itensFaker = new Faker<ItemPedidoCreateCommand>()
                .RuleFor(i => i.CodigoItem, f => f.Random.Guid())
                .RuleFor(i => i.Nome, f => f.Commerce.ProductName())
                .RuleFor(i => i.Preco, f => f.Random.Decimal(1, 1000))
                .RuleFor(i => i.Quantidade, f => f.Random.Int(1, 10));
                return itensFaker.Generate(3);
            });
            FinishWith((f, pedido) =>
            {
                var totalPedido = pedido.ItensPedido.Sum
                (i => i.Preco * i.Quantidade);
                pedido.Valor = totalPedido;
                pedido.Cobranca.ValorCobranca = totalPedido;
            });
        }
    }
    public class ClienteFaker : Faker<ClienteCreateCommand>
    {
        public ClienteFaker()
        {
            RuleFor(c => c.Nome, f => f.Person.FullName);
            RuleFor(c => c.Email, f => f.Person.Email);
            RuleFor(c => c.Cpf, f => f.Random.Replace("###.###.###-##"));
            RuleFor(c => c.Endereco, f => new EnderecoFaker().Generate());
        }
    }

    public class CobrancaFaker : Faker<CobrancaCreateCommand>
    {
        public CobrancaFaker()
        {
            RuleFor(c => c.NumeroCartao, f => f.Finance.CreditCardNumber());
            RuleFor(c => c.NomeImpressoNoCartao, f => f.Person.FullName);
            RuleFor(c => c.MesValidade, f => f.Random.Int(1, 12));
            RuleFor(c => c.AnoValidade, f => f.Random.Int(2023, 2030));
            RuleFor(c => c.CodigoSeguranca, f => f.Random.Int(100, 999));
            RuleFor(c => c.ValorCobranca, f => 0);
        }
    }
    public class EnderecoFaker : Faker<EnderecoCreateCommand>
    {
        public EnderecoFaker()
        {
            RuleFor(e => e.Logradouro, f => f.Address.StreetName());
            RuleFor(e => e.Complemento, f => f.Address.SecondaryAddress());
            RuleFor(e => e.Numero, f => f.Address.BuildingNumber());
            RuleFor(e => e.Bairro, f => f.Address.CitySuffix());
            RuleFor(e => e.Cidade, f => f.Address.City());
            RuleFor(e => e.Estado, f => f.Address.StateAbbr());
            RuleFor(e => e.Cep, f => f.Address.ZipCode());
        }
    }
}
