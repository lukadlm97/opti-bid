using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Messaging.Receving.Configuration;
using OptiBid.Microservices.Messaging.Receving.MessageQueue;
using OptiBid.Microservices.Shared.Messaging.DTOs;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using OptiBid.Microservices.Messaging.Receving.Factories;
using OptiBid.Microservices.Shared.Messaging.Enumerations;
using RabbitMQ.Client;

namespace OptiBid.Microservices.Messaging.Receving.Consumers
{
    public class AuctionConsumer : BackgroundService
    {
        private readonly IMqConnectionFactory _mqConnectionFactory;
        private readonly RabbitMqQueueSettings _queueNames;
        private readonly IAuctionMessageQueue _auctionMessageQueue;
        private readonly IBidMessageQueue _bidMessageQueue;
        private readonly ILogger<AuctionConsumer> _logger;

        public AuctionConsumer(IMqConnectionFactory mqConnectionFactory,
            IOptions<RabbitMqQueueSettings> options,
            IAuctionMessageQueue messageQueue,
            IBidMessageQueue bidMessageQueue,
            ILogger<AuctionConsumer> logger)
        {
            _mqConnectionFactory = mqConnectionFactory;
            _queueNames = options.Value;
            _auctionMessageQueue = messageQueue;
            _bidMessageQueue= bidMessageQueue;
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
            var message = JsonSerializer.Deserialize<Message>(content);
            switch (message.MessageType)
            {
                case MessageType.Auction:
                    _auctionMessageQueue.Write(message);
                    break;
                case MessageType.Bid:
                    _bidMessageQueue.Write(message);
                    break;
                case MessageType.Account:
                    default:
                        _logger.LogError("Unknown error occurred on consuming auction message queue");
                    break;

            }
        }

        async Task Consumer_Received(object sender, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
           // await _messageQueue.WriteAsync(JsonSerializer.Deserialize<Message>(message), default);
        }
    }
}
