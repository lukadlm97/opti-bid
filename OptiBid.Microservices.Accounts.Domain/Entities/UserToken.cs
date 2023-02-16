
namespace OptiBid.Microservices.Accounts.Domain.Entities
{
    public class UserToken
    {
        public int ID { get; set; }
        public string RefreshToken { get; set; }
        public bool IsValid { get; set; }
        public User? User { get; set; }
        public int? UserID { get; set; }
    }
}
