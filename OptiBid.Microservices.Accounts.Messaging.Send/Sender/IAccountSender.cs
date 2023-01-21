using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Accounts.Messaging.Send.Models;

namespace OptiBid.Microservices.Accounts.Messaging.Send.Sender
{
    public interface IAccountSender
    {
        void Send(AccountMessage message);
    }
}
