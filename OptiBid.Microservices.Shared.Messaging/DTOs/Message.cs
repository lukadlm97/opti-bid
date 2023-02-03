using OptiBid.Microservices.Shared.Messaging.Enumerations;
using System.Text.Json.Serialization;

namespace OptiBid.Microservices.Shared.Messaging.DTOs
{
    public class Message
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MessageType MessageType { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AccountMessage? AccountMessage { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AuctionMessage? AuctionMessage { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BidMessage? BidMessage { get; set; }
    }
}
