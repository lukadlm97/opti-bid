using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Contracts.Domain.Input
{
    public class ContactRequest
    {
        public int ContactId { get; set; }
        public int ContactTypeId { get; set; }
        public string Content { get; set; }
    }
}
