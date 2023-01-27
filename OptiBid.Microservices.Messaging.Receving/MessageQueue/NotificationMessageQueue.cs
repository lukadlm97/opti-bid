using System.Threading.Channels;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Messaging.Receving.Configuration;
using OptiBid.Microservices.Messaging.Receving.Models;
using OptiBid.Microservices.Messaging.Receving.Utilities;

namespace OptiBid.Microservices.Messaging.Receving.MessageQueue
{
    public class NotificationMessageQueue:IMessageQueue
    {
        private readonly ChannelSettings _channelSettings;
        private readonly Channel<NotificationMessage> _channel;
        private IObservable<NotificationMessage> _observable;


        public NotificationMessageQueue(IOptions<ChannelSettings> options)
        {
            _channelSettings = options.Value;
            _channel = Channel.CreateBounded<NotificationMessage>(new BoundedChannelOptions(_channelSettings.Capacity)
            {
                SingleReader = true,
                SingleWriter = false,
                AllowSynchronousContinuations = false,
                FullMode = BoundedChannelFullMode.Wait,
                Capacity = _channelSettings.Capacity
            });
            CreateVariable(default);
        }
        private void CreateVariable(CancellationToken cancellationToken = default)
        {
            this._observable = _channel.Reader.ToObservable(cancellationToken);
        }


        public ValueTask WriteAsync(NotificationMessage message, CancellationToken cancellationToken = default)
        {
            return _channel.Writer.WriteAsync(message, cancellationToken);
        }

        public IAsyncEnumerable<NotificationMessage> ReadAll(CancellationToken cancellationToken = default)
        {
            return _channel.Reader.ReadAllAsync(cancellationToken);
        }

        public IObservable<NotificationMessage> GetBaseEntities(CancellationToken cancellationToken = default)
        {

            return this._observable;

        }
    }
}
