using Microsoft.AspNetCore.SignalR;
using OptiBid.Microservices.Messaging.Receving.Models;

namespace OptiBid.API.Hubs
{
    public class NotificationHub:Hub
    {
        public NotificationHub()
        {
        }
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        async Task Send(NotificationMessage message)
        {
            await Clients.All.SendAsync(message.Content.ToString());
        }

    }
}
