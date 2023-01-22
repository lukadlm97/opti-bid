using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace OptiBid.Microservices.Auction.Services.Models
{
    public class CustomerResponse
    {
        public Enumerations.CreationStatus CreationStatus { get; set; }
        public Enumerations.SearchStatus SearchStatus { get; set; }
        public Domain.DTOs.Customer? Customer { get; set; } = default;
        public IEnumerable<Domain.DTOs.Customer> Customers { get; set; }
    }
}
