using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Auction.Services.Enumerations
{
    public enum CreationStatus
    {
        Unknown=0, 
        Success, 
        BadRequest, 
        Error,
        Exist
    }
}
