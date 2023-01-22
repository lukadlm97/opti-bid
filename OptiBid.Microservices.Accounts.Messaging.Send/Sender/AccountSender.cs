using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Accounts.Messaging.Send.Configurations;
using OptiBid.Microservices.Accounts.Messaging.Send.Models;
using RabbitMQ.Client;

namespace OptiBid.Microservices.Accounts.Messaging.Send.Sender
{
    public class AccountSender:IAccountSender
    {
        private readonly RabbitMqSettings _rabbitMqConfigs;
        private IConnection _connection;

        public AccountSender(IOptions<RabbitMqSettings> options)
        {
            _rabbitMqConfigs =options.Value;
            CreateConnection();
        }
        public async Task Send(AccountMessage message)
        {
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    //channel.QueueDeclare(queue: _rabbitMqConfigs.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var json =  JsonSerializer.Serialize(message);
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
