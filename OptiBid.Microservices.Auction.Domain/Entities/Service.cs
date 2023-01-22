

namespace OptiBid.Microservices.Auction.Domain.Entities
{
    public class Service:AuctionAsset
    {
        public ServiceCategory? ServiceCategory { get; set; }
        public  int? ServiceCategoryID { get; set; }

    }
}
