using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using OptiBid.Microservices.Auction.Messaging.Sender.Configurations;

namespace OptiBid.Microservices.Auction.Messaging.Sender.Factory
{
    public class RabbitMqConnectionFactory: IMqConnectionFactory
    {
        private readonly RabbitMqSettings _rabbitMqConfigs;
        private IConnection _connection;

        public RabbitMqConnectionFactory(IOptions<RabbitMqSettings> options)
        {
            _rabbitMqConfigs = options.Value;
            CreateConnection();
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

        public IConnection GetConnection()
        {
            if (ConnectionExists())
            {
                return _connection;
            }

            throw new InvalidOperationException();
        }
    }
}
