
namespace OptiBid.Microservices.Accounts.Domain.Entities
{
    public class Contact
    {
        public int ID { get; set; }
        public string? Content { get; set; }
        public bool IsActive { get; set; } 
        public int? UserID { get; set; }
        public User? User { get; set; }
        public int? ContactTypeID { get; set; }
        public ContactType? ContactType { get; set; }

    }
}
