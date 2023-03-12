using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Contracts.Domain.Input
{
    public class BidRequest
    {
        public decimal BidPrice { get; set; }
        public DateTime BidDate { get; set; }
    }
}
