using System.Reactive.Linq;
using OptiBid.API.Hubs;
using OptiBid.Microservices.Messaging.Receving.MessageQueue;
using OptiBid.Microservices.Shared.Messaging.DTOs;
using OptiBid.Microservices.Shared.Messaging.Enumerations;

namespace OptiBid.API.Producer
{
    public class NotificationService:BackgroundService
    {
        private readonly IMessageQueue _messageQueue;
        private readonly NotificationHub _notificationHub;

        public NotificationService(IMessageQueue messageQueue,NotificationHub notificationHub)
        {
            this._messageQueue = messageQueue;
            this._notificationHub = notificationHub;

        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(async () =>
            {
                foreach (var message in _messageQueue.GetBaseEntities(stoppingToken))
                {
                    await Handler(message, stoppingToken);
                }
            }, stoppingToken);
            return Task.CompletedTask;
          
        }

        async Task Handler(Message message,CancellationToken cancellationToken=default)
        {
            if (_notificationHub.Clients != null)
            {
                if (message.MessageType == MessageType.Account)
                {
                    await _notificationHub.SendAccountUpdate(message);
                }
                else
                {

                    await _notificationHub.SendAuctionUpdate(message);
                }

            }
        }
    }
}
