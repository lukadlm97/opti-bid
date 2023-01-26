
namespace OptiBid.Microservices.Auction.Domain.Input
{
    public class Bid
    {
        public decimal BidPrice { get; set; }
        public DateTime BidDate { get; set; }
        public int AuctionAssetId { get; set; }
        public string CustomerUsername { get; set; }
    }
}
