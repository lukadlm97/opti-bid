using Microsoft.AspNetCore.SignalR;
using OptiBid.Microservices.Messaging.Receving.MessageQueue;
using OptiBid.Microservices.Messaging.Receving.Models;

namespace OptiBid.API.Hubs
{
    public class NotificationHub:Hub
    {
        async Task Send(NotificationMessage message)
        {
            await Clients.All.SendAsync(message.Content.ToString());
        }

    }
}
