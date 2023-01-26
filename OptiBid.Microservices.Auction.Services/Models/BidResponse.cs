

namespace OptiBid.Microservices.Auction.Services.Models
{
    public class BidResponse
    {
        public Enumerations.CreationStatus CreationStatus { get; set; }
        public Enumerations.SearchStatus SearchStatus { get; set; }
        public Domain.DTOs.Bid? Bid { get; set; }
        public IEnumerable<Domain.DTOs.Bid>? Bids { get; set; }
    }
}
