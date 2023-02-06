using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Messaging.Receving.Configuration;
using OptiBid.Microservices.Messaging.Receving.Factories;
using OptiBid.Microservices.Messaging.Receving.MessageQueue;
using OptiBid.Microservices.Messaging.Receving.Models;
using OptiBid.Microservices.Shared.Messaging.DTOs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OptiBid.Microservices.Messaging.Receving.Consumer
{
    public class AuthenticationConsumer:BackgroundService
    {
        private readonly IMqConnectionFactory _mqConnectionFactory;
        private readonly RabbitMqQueueSettings _queueNames;
        private readonly IMessageQueue _messageQueue;

        public AuthenticationConsumer(IMqConnectionFactory mqConnectionFactory,
            IOptions<RabbitMqQueueSettings> options,
            IMessageQueue messageQueue)
        {
            _mqConnectionFactory = mqConnectionFactory;
            _queueNames = options.Value;
            _messageQueue = messageQueue;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                var connection = _mqConnectionFactory.GetConnection();
                using (var channel = connection.CreateModel())
                {

                    var consumer = new AsyncEventingBasicConsumer(channel);
                    consumer.Received += Consumer_Recived;
                    channel.BasicConsume(_queueNames.AuctionQueueName, true, consumer);
                }
            }
        }

        async Task Consumer_Recived(object sender, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            await _messageQueue.WriteAsync(JsonSerializer.Deserialize<Message>(message), default);
        }
    }
}
