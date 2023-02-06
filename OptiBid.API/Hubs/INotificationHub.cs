namespace OptiBid.API.Hubs
{
    public interface INotificationHub
    {
        Task ReceiveNotificationMessage(string user, string message);
    }
}
