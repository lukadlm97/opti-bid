using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Contracts.Domain.Input
{
    public class UpsertAssetRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Closed { get; set; }
        public bool Started { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<string> MediaUrls { get; set; }
        public int CustomerId { get; set; }
        public int ProductTypeId { get; set; } = -1;
        public int ServiceTypeId { get; set; } = -1;
    }
}
