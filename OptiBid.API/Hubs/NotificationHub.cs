using Microsoft.AspNetCore.SignalR;
using OptiBid.Microservices.Messaging.Receving.Models;

namespace OptiBid.API.Hubs
{
    public class NotificationHub:Hub<INotificationHub>
    {
        private readonly ConnectionManager _connectionManager;

        public NotificationHub(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

       
     
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _connectionManager.RemoveConnection(Context.ConnectionId, "auctions");
            _connectionManager.RemoveConnection(Context.ConnectionId, "accounts");
            return base.OnDisconnectedAsync(exception);
        }


    }
}
