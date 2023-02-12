using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using OptiBid.Microservices.Messaging.Receving.Models;
using OptiBid.Microservices.Messaging.Receving.Utilities;
using OptiBid.Microservices.Shared.Messaging.DTOs;

namespace OptiBid.API.Hubs
{
    public class NotificationHub:Hub
    {
        private readonly ConnectionManager _connectionManager;

        public NotificationHub(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
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

        public async Task SendAccountUpdate(Message message,CancellationToken cancellationToken)
        {
            foreach (var connection in _connectionManager.GetConnections("account"))
            {
                var messageForClient  = message.GetAccountNotificationMessage();
                await Clients.Client(connection).SendAsync("ReceiveAccountUpdate", messageForClient, cancellationToken);
            }
        }

        public async Task SendAuctionAssetsUpdate(Message message, CancellationToken cancellationToken)
        {
            foreach (var connection in _connectionManager.GetConnections("auction"))
            {
                var messageForClient = message.GetAuctionAssetsNotificationMessage();
                await Clients.Client(connection).SendAsync("ReceiveAuctionAssetUpdate",  messageForClient.Item1,messageForClient.Item2, cancellationToken);
            }
        }

        public async Task SendAuctionBidUpdate(Message message, CancellationToken cancellationToken)
        {
            foreach (var connection in _connectionManager.GetConnections("bid"))
            {
                var messageForClient = message.GetAssetsBidNotificationMessage();
                await Clients.Client(connection).SendAsync("ReceiveAuctionBidUpdate", messageForClient.Item1, messageForClient.Item2,messageForClient.Item3, cancellationToken);
            }
        }

    }
}
