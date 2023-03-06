using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Contracts.Domain.Input
{
    public record ContactRequest(int ContactTypeId, string Content);
}
