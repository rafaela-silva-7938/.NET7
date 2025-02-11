using ApiPedidos.Application.Events;
using ApiPedidos.Application.Interfaces;
using ApiPedidos.Infra.EventBus.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Infra.EventBus.Producers
{
    public class PedidoProducer : IPedidoProducer
    {
        private readonly MessageBrokerSettings? _messageBrokerSettings;

        //IOptions<T>: Acessa as configurações como propriedades em vez de strings mágicas
        public PedidoProducer
        (IOptions<MessageBrokerSettings>? messageBrokerSettings)
        {
            _messageBrokerSettings = messageBrokerSettings.Value;
        }

        //jogando o pedido pra fila no Rabbit
        public async Task Add(PedidoRealizadoEvent @event)
        {
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(_messageBrokerSettings.Url)
            };
            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                    queue: _messageBrokerSettings.Queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );
                    await Task.Run(() => channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: _messageBrokerSettings.Queue,
                    basicProperties: null,
                    body: Encoding.UTF8.GetBytes
                    (JsonConvert.SerializeObject(@event))
                    ));
                }
            }
        }
    }
}