using OptiBid.Microservices.Shared.Messaging.Enumerations;
using System.Text.Json.Serialization;

namespace OptiBid.Microservices.Shared.Messaging.DTOs
{
    public class AccountMessage
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AccountMessageType? AccountMessageType { get; set; } = null;
        public string UserName { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
    }
}
