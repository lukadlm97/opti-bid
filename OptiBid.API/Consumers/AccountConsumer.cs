using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Messaging.Receving.Configuration;
using OptiBid.Microservices.Messaging.Receving.Factories;
using OptiBid.Microservices.Messaging.Receving.MessageQueue;
using OptiBid.Microservices.Messaging.Receving.Models;
using OptiBid.Microservices.Shared.Messaging.DTOs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OptiBid.API.Consumers
{
    public class AccountConsumer : BackgroundService
    {
        private readonly IMqConnectionFactory _mqConnectionFactory;
        private readonly RabbitMqQueueSettings _queueNames;
        private readonly IAccountMessageQueue _messageQueue;
        private readonly ILogger<AuctionConsumer> _logger;

        public AccountConsumer(IMqConnectionFactory mqConnectionFactory,
            IOptions<RabbitMqQueueSettings> options,
            IAccountMessageQueue messageQueue,
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
            channel.BasicConsume(_queueNames.AccountsQueueName, false, consumer);

            return Task.CompletedTask;

        }
        private void HandleMessage(string content)
        {
            _logger.LogInformation($"consumer received {content}");
            _messageQueue.Write(JsonSerializer.Deserialize<Message>(content));
        }

        async Task Consumer_Received(object sender, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            await _messageQueue.WriteAsync(JsonSerializer.Deserialize<Message>(message), default);
        }
    }

    /*: BackgroundService
{
    private readonly IMqConnectionFactory _mqConnectionFactory;
    private readonly RabbitMqQueueSettings _queueNames;
    private readonly IMessageQueue _messageQueue;

    public AccountConsumer(IMqConnectionFactory mqConnectionFactory,
        IOptions<RabbitMqQueueSettings> options,
        IMessageQueue messageQueue)
    {
        _mqConnectionFactory = mqConnectionFactory;
        _queueNames = options.Value;
        _messageQueue = messageQueue;

    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task.Run(async () =>
        {
            await DoWork(stoppingToken);
        }, stoppingToken);
        return Task.CompletedTask;
    }
    async Task DoWork(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var connection = _mqConnectionFactory.GetConnection();
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        _messageQueue.Write(JsonSerializer.Deserialize<Message>(message));
                    };
                    channel.BasicConsume(_queueNames.AccountsQueueName, true, consumer);
                }

                await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }

}*/
}
