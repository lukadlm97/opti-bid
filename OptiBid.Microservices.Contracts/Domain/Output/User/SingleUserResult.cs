using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Contracts.Domain.Output.User
{
    public class SingleUserResult
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public bool FirstLogIn { get; set; }
    }
}
