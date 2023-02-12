using OptiBid.Microservices.Shared.Messaging.DTOs;

namespace OptiBid.Microservices.Messaging.Receving.MessageQueue
{
    public interface IMessageQueue
    {
        void Write(Message message);
        ValueTask WriteAsync(Message message,
            CancellationToken cancellationToken = default);

        IAsyncEnumerable<Message> ReadAll(CancellationToken cancellationToken = default);

        IObservable<Message> GetBaseEntities(CancellationToken cancellationToken = default);
    }
}
