using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Accounts.Domain.Input
{
    public class ContactModel
    {
        public int ContactId { get; set; }
        public int ContactTypeId { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
