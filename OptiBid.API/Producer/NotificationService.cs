using System.Reactive.Linq;
using OptiBid.API.Hubs;
using OptiBid.Microservices.Messaging.Receving.MessageQueue;
using OptiBid.Microservices.Messaging.Receving.Models;
using OptiBid.Microservices.Shared.Messaging.DTOs;
using OptiBid.Microservices.Shared.Messaging.Enumerations;

namespace OptiBid.API.Producer
{
    public class NotificationService:BackgroundService
    {
        private readonly IMessageQueue _messageQueue;
        private readonly NotificationHub _notificationHub;
        private readonly ConnectionManager _connectionManager;

        public NotificationService(IMessageQueue messageQueue,NotificationHub notificationHub,ConnectionManager connectionManager)
        {
            this._messageQueue = messageQueue;
            this._notificationHub = notificationHub;
            this._connectionManager = connectionManager;

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
                    var selectedClients = _connectionManager.GetConnections("account");
                    foreach (var selectedClient in selectedClients)
                    {

                       await _notificationHub.Clients.Client(selectedClient).SendCoreAsync("", 
                            new[] { message },cancellationToken);
                    }
                }
                else
                {
                    var selectedClients = _connectionManager.GetConnections("auction");
                    foreach (var selectedClient in selectedClients)
                    {

                        await _notificationHub.Clients.Client(selectedClient).SendCoreAsync("",
                            new[] { message }, cancellationToken);
                    }
                }

            }
        }
    }
}
