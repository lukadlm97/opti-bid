using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Auction.Messaging.Sender.Models
{
    public class BidMessage
    {
        public string Bider { get; set; }
        public decimal BidPrice { get; set; }
        public DateTime BidDateTime { get; set; }
        public int AssetId { get; set; }
        public string AssetName { get; set; }
    }
}
