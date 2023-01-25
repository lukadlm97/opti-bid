using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Auction.Messaging.Sender.Configurations;
using OptiBid.Microservices.Auction.Messaging.Sender.Models;
using RabbitMQ.Client;

namespace OptiBid.Microservices.Auction.Messaging.Sender.Sender
{
    public class AuctionAssetsSender:IAuctionAssetsSender
    {
        private readonly RabbitMqSettings _rabbitMqConfigs;
        private IConnection _connection;

        public AuctionAssetsSender(IOptions<RabbitMqSettings> options)
        {
            _rabbitMqConfigs = options.Value;
            CreateConnection();
        }
        public async Task Send(AuctionAssetMessage message)
        {
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    //channel.QueueDeclare(queue: _rabbitMqConfigs.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var json = JsonSerializer.Serialize(message);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "", routingKey: _rabbitMqConfigs.QueueName, basicProperties: null, body: body);
                }
            }
        }
        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _rabbitMqConfigs.Hostname,
                    UserName = _rabbitMqConfigs.UserName,
                    Password = _rabbitMqConfigs.Password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }
    }
}
