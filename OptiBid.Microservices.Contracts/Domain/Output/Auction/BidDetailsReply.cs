
namespace OptiBid.Microservices.Contracts.Domain.Output.Auction
{
    public class BidDetailsReply
    {
        public  int BidId { get; set; }
        public  decimal BidPrice { get; set; }
        public  DateTime BidDate { get; set; }
        public  int AuctionAssetsId { get; set; }
        public  int CustomerId { get; set; }
    }
}
