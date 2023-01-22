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
        public string Description { get; set; }
        public bool Closed { get; set; } = false;
        public bool Started { get; set; } = false;
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Bid>    Bids { get; set; }
        public ICollection<MediaUrl> MediaUrls { get; set; }

    }
}
