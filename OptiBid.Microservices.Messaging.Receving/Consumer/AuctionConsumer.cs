using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Messaging.Receving.Configuration;
using OptiBid.Microservices.Messaging.Receving.MessageQueue;
using OptiBid.Microservices.Shared.Messaging.DTOs;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OptiBid.Microservices.Messaging.Receving.Factories;
using RabbitMQ.Client;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;

namespace OptiBid.Microservices.Messaging.Receving.Consumer
{
    public class AuctionConsumer : BackgroundService
    {
        private readonly IMqConnectionFactory _mqConnectionFactory;
        private readonly RabbitMqQueueSettings _queueNames;
        private readonly IAuctionMessageQueue _messageQueue;
        private readonly ILogger<AuctionConsumer> _logger;

        public AuctionConsumer(IMqConnectionFactory mqConnectionFactory,
            IOptions<RabbitMqQueueSettings> options,
            IAuctionMessageQueue messageQueue,
            ILogger<AuctionConsumer> logger)
        {
            _mqConnectionFactory = mqConnectionFactory;
            _queueNames = options.Value;
            _messageQueue = messageQueue;
            _logger = logger;

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var connection = _mqConnectionFactory.GetConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                // received message
                var body = ea.Body.ToArray();
                var content = System.Text.Encoding.UTF8.GetString(body);

                // handle the received message  
                HandleMessage(content);
                channel.BasicAck(ea.DeliveryTag, false);
            };
            channel.BasicConsume(_queueNames.AuctionQueueName, false, consumer);

            return Task.CompletedTask;

        }

        private void HandleMessage(string content)
        {
            _logger.LogInformation($"consumer received {content}");
            _messageQueue.Write(JsonSerializer.Deserialize<Message>(content));
        }
    }
}
