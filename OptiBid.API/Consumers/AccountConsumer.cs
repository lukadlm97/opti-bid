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

    }
}
