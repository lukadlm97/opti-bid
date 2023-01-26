using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Auction.Messaging.Sender.Models
{
    public class AuctionAssetMessage
    {
        public int ID { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Name { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Description { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AssetType? AssetType { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ActionType ActionType { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Username { get; set; }
    }

    public enum ActionType
    {
        Added,
        Updated,
        Deleted
    }

    
}
