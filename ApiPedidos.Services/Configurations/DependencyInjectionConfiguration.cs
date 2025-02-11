using ApiPedidos.Application.Interfaces;
using ApiPedidos.Application.Services;
using ApiPedidos.Domain.Interfaces.Repositories;
using ApiPedidos.Domain.Interfaces.Services;
using ApiPedidos.Domain.Services;
using ApiPedidos.Infra.Data.Contexts;
using ApiPedidos.Infra.Data.Repositories;
using ApiPedidos.Infra.EventBus.Producers;
using ApiPedidos.Infra.EventBus.Settings;
using Microsoft.EntityFrameworkCore;
namespace ApiPedidos.Services.Configurations
{
    public class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjection
        (WebApplicationBuilder builder)
        {
            builder.Services.Configure<MessageBrokerSettings>
            (builder.Configuration.GetSection("MessageBrokerSettings"));
            builder.Services.AddTransient<IPedidoAppService, PedidoAppService>();
            builder.Services.AddTransient<IPedidoDomainService, PedidoDomainService>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IPedidoProducer, PedidoProducer>();
          
            // Adicionando o DataContext com SQL Server
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Conexao")));

        }
    }
}