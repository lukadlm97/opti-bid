using System.Threading;
using System.Threading.Channels;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Messaging.Receving.Configuration;
using OptiBid.Microservices.Messaging.Receving.Models;
using OptiBid.Microservices.Messaging.Receving.Utilities;
using OptiBid.Microservices.Shared.Messaging.DTOs;

namespace OptiBid.Microservices.Messaging.Receving.MessageQueue
{
    public class NotificationMessageQueue:IMessageQueue
    {
        private readonly ChannelSettings _channelSettings;
        private readonly Channel<Message> _channel;
        private IObservable<Message> _observable;


        public NotificationMessageQueue(IOptions<ChannelSettings> options)
        {
            _channelSettings = options.Value;
            _channel = Channel.CreateBounded<Message>(new BoundedChannelOptions(_channelSettings.Capacity)
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


        public void Write(Message message)
        {
             _channel.Writer.TryWrite(message);
        }

        public ValueTask WriteAsync(Message message, CancellationToken cancellationToken = default)
        {
            return _channel.Writer.WriteAsync(message, cancellationToken);
        }

        public IAsyncEnumerable<Message> ReadAll(CancellationToken cancellationToken = default)
        {
            return _channel.Reader.ReadAllAsync(cancellationToken);
        }

        public IObservable<Message> GetBaseEntities(CancellationToken cancellationToken = default)
        {

            return this._observable;

        }
    }
}
