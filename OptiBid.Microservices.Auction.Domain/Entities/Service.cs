using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Auction.Domain.Entities
{
    public class Service:AuctionAsset
    {
        public ServiceCategory? Category { get; set; }
        public  int? CategoryID { get; set; }

    }
}
