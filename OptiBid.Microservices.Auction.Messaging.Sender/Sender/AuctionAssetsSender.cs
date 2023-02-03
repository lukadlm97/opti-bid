using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Auction.Messaging.Sender.Configurations;
using OptiBid.Microservices.Auction.Messaging.Sender.Factory;
using OptiBid.Microservices.Shared.Messaging.DTOs;
using OptiBid.Microservices.Shared.Messaging.Enumerations;
using RabbitMQ.Client;

namespace OptiBid.Microservices.Auction.Messaging.Sender.Sender
{
    public class AuctionAssetsSender : IAuctionAssetsSender
    {
        private readonly IMqConnectionFactory _mqConnectionFactory;
        private readonly MqSettings _mqSettings;

        public AuctionAssetsSender(IMqConnectionFactory mqConnectionFactory, IOptions<MqSettings> options)
        {
            _mqConnectionFactory = mqConnectionFactory;
            _mqSettings = options.Value;
        }
        public async Task Send(AuctionMessage message)
        {

            using (var channel = _mqConnectionFactory.GetConnection().CreateModel())
            {
                //channel.QueueDeclare(queue: _rabbitMqConfigs.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var transferMessage = new Message()
                {
                    AuctionMessage = message,
                    MessageType = MessageType.Auction
                };
                var json = JsonSerializer.Serialize(transferMessage);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: _mqSettings.QueueName, basicProperties: null, body: body);
            }

        }
    }
}
