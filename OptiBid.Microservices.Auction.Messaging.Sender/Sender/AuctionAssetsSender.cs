﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Auction.Messaging.Sender.Configurations;
using OptiBid.Microservices.Auction.Messaging.Sender.Factory;
using OptiBid.Microservices.Auction.Messaging.Sender.Models;
using RabbitMQ.Client;

namespace OptiBid.Microservices.Auction.Messaging.Sender.Sender
{
    public class AuctionAssetsSender:IAuctionAssetsSender
    {
        private readonly IMqConnectionFactory _mqConnectionFactory;
        private readonly MqSettings _mqSettings;

        public AuctionAssetsSender(IMqConnectionFactory mqConnectionFactory,IOptions<MqSettings> options)
        {
            _mqConnectionFactory = mqConnectionFactory;
            _mqSettings = options.Value;
        }
        public async Task Send(AuctionAssetMessage message)
        {
          
                using (var channel = _mqConnectionFactory.GetConnection().CreateModel())
                {
                    //channel.QueueDeclare(queue: _rabbitMqConfigs.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var json = JsonSerializer.Serialize(message);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "", routingKey: _mqSettings.QueueName, basicProperties: null, body: body);
                }
            
        }
    }
}
