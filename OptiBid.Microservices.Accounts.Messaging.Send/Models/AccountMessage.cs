

using System.Text.Json.Serialization;

namespace OptiBid.Microservices.Accounts.Messaging.Send.Models
{
    public class AccountMessage
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MessageType MessageType { get; set; } = MessageType.Registration;
        public string UserName { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
    }

    public enum MessageType
    {
        Registration,
        Update
    }
}
