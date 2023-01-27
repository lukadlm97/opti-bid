
using RabbitMQ.Client;

namespace OptiBid.Microservices.Messaging.Receving.Factories
{
    public interface IMqConnectionFactory
    {
        IConnection GetConnection();
    }
}
