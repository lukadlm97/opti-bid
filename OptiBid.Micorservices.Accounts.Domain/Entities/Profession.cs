using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Accounts.Domain.Entities
{
    public class Profession
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Skill> Skills { get; set; }
    }
}
