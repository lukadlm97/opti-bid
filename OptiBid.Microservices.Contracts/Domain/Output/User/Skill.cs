using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Contracts.Domain.Output.User
{
    public class Skill
    {
        public int Id { get; set; }
        public int? ProfessionId { get; set; }
        public bool IsActive { get; set; }
    }
}
