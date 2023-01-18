using System.Security.AccessControl;

namespace OptiBid.Microservices.Accounts.Domain.Entities
{
    public class User
    {
        public  int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int? UserRoleID { get; set; }
        public UserRole? UserRole { get; set; }
        public int? CountryID { get; set; }
        public Country? Country { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public ICollection<Contact> Contacts { get; set; }

    }
}
