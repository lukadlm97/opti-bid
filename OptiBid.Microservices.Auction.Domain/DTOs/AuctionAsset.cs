using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Auction.Domain.DTOs
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
        public int? ProductTypeId { get; set; }
        public int? ServiceTypeId { get; set; }
        public IEnumerable<string> MediaUrls { get; set; }
    }
}
