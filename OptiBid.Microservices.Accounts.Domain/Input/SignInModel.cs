using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Accounts.Domain.Input
{
    public class SignInModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
