using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Auction.Domain.Entities
{
    public class Product:AuctionAsset
    {
        public int? ProductCategoryID { get; set; }
        public ProductCategory? ProductCategory { get; set; }

    }
}
