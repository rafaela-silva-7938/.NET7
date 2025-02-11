using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiPedidos.Infra.EventBus.Settings
{
    //Classe para capturar as configurações feitas no arquivo appsettings.json
    public class MessageBrokerSettings
    {
        public string? Url { get; set; }
        public string? Queue { get; set; }
    }
}