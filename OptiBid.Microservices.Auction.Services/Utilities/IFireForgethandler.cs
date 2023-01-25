
using OptiBid.Microservices.Auction.Messaging.Sender.Sender;

namespace OptiBid.Microservices.Auction.Services.Utilities
{
    public interface IFireForgetHandler
    {
        void Execute(Func<IAuctionAssetsSender, Task> asyncWork);
    }
}
