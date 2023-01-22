using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Auction.Domain.Entities
{
    public class Bid
    {
        public int ID { get; set; }
        public decimal  BidPrice { get; set; }
        public DateTime BidDate { get; set; }
        public AuctionAsset Asset { get; set; }
        public int AuctionAssetId { get; set; }
        public Customer Customer { get; set; }
        public  int CustomerId { get; set; }
    }
}
