

namespace OptiBid.Microservices.Auction.Domain.Input
{
    public class AuctionAsset
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Closed { get; set; } = false;
        public bool Started { get; set; } = false;
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<string> MediaUrl { get; set; }
        public int? ProductTypeId { get; set; }
        public int? ServiceTypeId { get; set; }
        public int CustomerId { get; set; }
    }
}
