using OptiBid.Microservices.Auction.Messaging.Sender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Auction.Messaging.Sender.Sender
{
    public interface IAuctionAssetsSender
    {
        Task Send(AuctionAssetMessage message);
    }
}
