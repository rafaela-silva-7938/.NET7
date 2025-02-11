using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPedidos.Application.Events
{    public abstract class BaseEvent
    {
        public Guid? EventId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}