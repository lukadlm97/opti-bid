using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Accounts.Domain.DTOs
{
    public class Skill
    {
        public int Id { get; set; }
        public int? ProfessionId { get; set; }
        public bool IsActive { get; set; }
    }
}
