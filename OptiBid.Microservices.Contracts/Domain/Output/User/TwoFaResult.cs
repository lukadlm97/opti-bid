using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Contracts.Domain.Output.User
{
    public class TwoFaResult
    {
        public string QrCode { get; set; }
        public string ManualEntryKey { get; set; }
    }
}
