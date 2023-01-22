using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Auction.Domain.Entities
{
    public class AuctionAsset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Closed { get; set; } = false;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Bid>    Bids { get; set; }

    }
}
