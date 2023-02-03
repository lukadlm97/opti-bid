
using OptiBid.Microservices.Shared.Messaging.DTOs;

namespace OptiBid.Microservices.Auction.Messaging.Sender.Sender
{
    public interface IBidSender
    {
        Task Send(BidMessage message);
    }
}
