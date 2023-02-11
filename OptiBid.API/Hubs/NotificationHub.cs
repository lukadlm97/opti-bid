using Microsoft.AspNetCore.SignalR;
using OptiBid.Microservices.Messaging.Receving.MessageQueue;
using OptiBid.Microservices.Messaging.Receving.Models;
using OptiBid.Microservices.Shared.Messaging.DTOs;
using OptiBid.Microservices.Shared.Messaging.Enumerations;
using System.Threading;

namespace OptiBid.API.Hubs
{
    public class NotificationHub:Hub
    {
        private readonly ConnectionManager _connectionManager;
        private readonly IMessageQueue _notificationMessageQueue;

        public NotificationHub(ConnectionManager connectionManager,IMessageQueue notificationMessageQueue)
        {
            _connectionManager = connectionManager;
            _notificationMessageQueue = notificationMessageQueue;
        }

        public async Task<string> Subscribe(string topic)
        {
            _connectionManager.AddConnection(Context.ConnectionId,topic);
            return "You successfully subscribed on topic: " + topic;

         //   await Clients.Client(Context.ConnectionId).SendAsync("Subscribe");
        }

        public async Task<string> Unsubscribe(string topic)
        {
            _connectionManager.RemoveConnection(Context.ConnectionId, topic);
            return "You successfully unsubscribed from topic: " + topic;
        }

        public async IAsyncEnumerable<Message> SendAccountUpdate(CancellationToken cancellationToken)
        {
            while (true)
            {
                await foreach (var message in (_notificationMessageQueue.ReadAll(cancellationToken)))
                {
                    if (message != null && message.MessageType==MessageType.Account && _connectionManager.IsSubscribed(Context.ConnectionId, "account"))
                        yield return message;
                }

                if (cancellationToken.IsCancellationRequested)
                {

                    yield break;
                }
            }
        }

        public async IAsyncEnumerable<Message> SendAuctionUpdate(CancellationToken cancellationToken)
        {
            while (true)
            {
                await foreach (var message in (_notificationMessageQueue.ReadAll(cancellationToken)))
                {
                    if (message != null && message.MessageType != MessageType.Account && _connectionManager.IsSubscribed(Context.ConnectionId, "auction"))
                        yield return message;
                }

                if (cancellationToken.IsCancellationRequested)
                {

                    yield break;
                }
            }
        }

    }
}
