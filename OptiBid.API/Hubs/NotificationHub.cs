using Microsoft.AspNetCore.SignalR;
using OptiBid.Microservices.Messaging.Receving.Models;
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

        public async Task SendAccountUpdate(Message message)
        {
           var connections =  _connectionManager.GetConnections("account");
           foreach (var connection in connections)
           {
               await Clients.Client(connection).SendAsync("ReceiveAccountUpdate", message);
           }
        }

        public async Task SendAuctionUpdate(Message message)
        {
            var connections = _connectionManager.GetConnections("auction");
            foreach (var connection in connections)
            {
                await Clients.Client(connection).SendAsync("ReceiveAuctionUpdate", message);
            }
        }

    }
}
