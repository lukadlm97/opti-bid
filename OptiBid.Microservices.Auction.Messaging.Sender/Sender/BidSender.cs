
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
    public class BidSender:IBidSender
    {
        private IMqConnectionFactory _connectionFactory;
        private readonly MqSettings _mqSettings;

        public BidSender(IOptions<MqSettings> options, IMqConnectionFactory connectionFactory)
        {
            _mqSettings = options.Value;
            _connectionFactory=connectionFactory;
        }
        public async Task Send(BidMessage message)
        {
    
                using (var channel = _connectionFactory.GetConnection().CreateModel())
                {
                //channel.QueueDeclare(queue: _rabbitMqConfigs.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var transferMessage = new Message()
                {
                    BidMessage = message,
                    MessageType = MessageType.Bid
                };
                var json = JsonSerializer.Serialize(transferMessage);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "", routingKey: _mqSettings.QueueName, basicProperties: null, body: body);
                }
            
        }
        
    }
}
