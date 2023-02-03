using OptiBid.Microservices.Shared.Messaging.DTOs;

namespace OptiBid.Microservices.Accounts.Messaging.Send.Sender
{
    public interface IAccountSender
    {
        Task Send(AccountMessage message);
    }
}
