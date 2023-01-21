using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Accounts.Domain.DTOs
{
    public class Contact
    {
        public int Id { get; set; }
        public int? ContactTypeId { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
    }
}
