using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Accounts.Domain.Entities
{
    public class UserRole
    {
        public  int ID { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; } = false;
        public ICollection<User> Users { get; set; }
    }
}
