﻿

namespace OptiBid.Microservices.Auction.Messaging.Sender.Configurations
{
    public class RabbitMqSettings
    {
        public string Hostname { get; set; }

        public string QueueName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
