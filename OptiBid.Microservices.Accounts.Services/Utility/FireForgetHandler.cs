using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OptiBid.Microservices.Accounts.Messaging.Send.Sender;

namespace OptiBid.Microservices.Accounts.Services.Utility
{
    public class FireForgetHandler:IFireForgetHandler
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public FireForgetHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public void Execute(Func<IAccountSender, Task> asyncWork)
        {
            Task.Run(async () =>
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var sender = scope.ServiceProvider.GetRequiredService<IAccountSender>();
                    await asyncWork(sender);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }
    }
}
