

namespace OptiBid.Microservices.Auction.Services.Models
{
    public class AuctionAssetsResponse
    {
        public Enumerations.CreationStatus CreationStatus { get; set; }
        public Enumerations.SearchStatus SearchStatus { get; set; }
        public Domain.DTOs.AuctionAsset? Asset { get; set; } 
        public  IEnumerable<Domain.DTOs.AuctionAsset>? Assets { get; set; }
    }
}
