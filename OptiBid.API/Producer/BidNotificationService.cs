using System.Reactive.Linq;
using OptiBid.API.Hubs;
using OptiBid.Microservices.Messaging.Receving.MessageQueue;
using OptiBid.Microservices.Shared.Messaging.DTOs;

namespace OptiBid.API.Producer
{
    public class BidNotificationService : BackgroundService
    {
        private readonly IBidMessageQueue _messageQueue;
        private readonly NotificationHub _notificationHub;

        public BidNotificationService(IBidMessageQueue messageQueue, NotificationHub notificationHub)
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

        async Task Handler(Message message, CancellationToken cancellationToken = default)
        {
            if (_notificationHub.Clients != null)
            {
                await _notificationHub.SendAuctionBidUpdate(message, cancellationToken);
            }
        }
    }
}
