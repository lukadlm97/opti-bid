
namespace OptiBid.Microservices.Auction.Domain.DTOs
{
    public class Bid
    {
        public int ID { get; set; }
        public decimal BidPrice { get; set; }
        public DateTime BidDate { get; set; }
        public int AuctionAssetId { get; set; }
        public int CustomerId { get; set; }
    }
}
