
namespace OptiBid.Microservices.Accounts.Domain.Entities
{
    public class ContactType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}
