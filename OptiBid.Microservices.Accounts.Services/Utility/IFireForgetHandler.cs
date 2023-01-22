using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Accounts.Messaging.Send.Sender;

namespace OptiBid.Microservices.Accounts.Services.Utility
{
    public interface IFireForgetHandler
    {
        void Execute(Func<IAccountSender, Task> asyncWork);
    }
}
