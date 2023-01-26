using OptiBid.Microservices.Auction.Messaging.Sender.Models;
namespace OptiBid.Microservices.Auction.Messaging.Sender.Sender
{
    public interface IBidSender
    {
        Task Send(BidMessage message);
    }
}
