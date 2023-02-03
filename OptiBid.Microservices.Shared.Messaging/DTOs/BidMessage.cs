namespace OptiBid.Microservices.Shared.Messaging.DTOs
{
    public class BidMessage
    {
        public string Bider { get; set; }
        public decimal BidPrice { get; set; }
        public DateTime BidDateTime { get; set; }
        public int AssetId { get; set; }
        public string AssetName { get; set; }
    }
}
