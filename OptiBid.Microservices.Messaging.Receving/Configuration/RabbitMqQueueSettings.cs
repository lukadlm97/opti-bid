using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Messaging.Receving.Configuration
{
    public class RabbitMqQueueSettings
    {
        public string AccountsQueueName { get; set; }
        public string AuctionQueueName { get; set; }
    }
}
