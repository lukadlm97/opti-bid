using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Accounts.Domain.Input
{
    public class SkillModel
    {
        public int ProfessionId { get; set; }
        public int SkillId { get; set; } = 0;
        public bool IsActive { get; set; } = true;
    }
}
