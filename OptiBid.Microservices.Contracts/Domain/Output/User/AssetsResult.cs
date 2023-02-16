using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Contracts.Domain.Output.User
{
    public class AssetsResult
    {
        public string RefreshToken { get; set; }
        public string TwoFaSource { get; set; }
    }
}
