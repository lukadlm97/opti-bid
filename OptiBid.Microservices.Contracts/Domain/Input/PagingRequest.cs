using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Contracts.Domain.Input
{
    public record PagingRequest(string filter = default, int page = 1, int size = 10);
}
