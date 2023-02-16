using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Accounts.Domain.DTOs
{
    public class UserDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public int UserRoleID { get; set; }
        public int CountryId { get; set; }
        public bool FirstLogIn { get; set; } 
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
    }
}
