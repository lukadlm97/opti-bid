

namespace OptiBid.Microservices.Accounts.Domain.Entities
{
    public class UserTwoFAAssets
    {
        public int ID { get; set; }
        public string Source { get; set; }
        public User? User { get; set; }
        public int? UserID { get; set; }

    }
}
