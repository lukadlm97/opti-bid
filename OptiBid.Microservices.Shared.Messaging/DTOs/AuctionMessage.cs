using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OptiBid.Microservices.Shared.Messaging.Enumerations;

namespace OptiBid.Microservices.Shared.Messaging.DTOs
{
    public class AuctionMessage
    {
        public int ID { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Name { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Description { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AssetMessageType? AssetType { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AuctionMessageType ActionType { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Username { get; set; }
    }
}
