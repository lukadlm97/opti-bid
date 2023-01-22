using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Auction.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public DateTime DateOpened { get; set; }
        public ICollection<Bid> Bids { get; set; }

    }
}
