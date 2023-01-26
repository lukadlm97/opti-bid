using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Auction.Messaging.Sender.Sender;

namespace OptiBid.Microservices.Auction.Services.Utilities
{
    public class FireForgetHandler : IFireForgetHandler
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public FireForgetHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public void Execute(Func<IAuctionAssetsSender, Task> asyncWork)
        {
            Task.Run(async () =>
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var sender = scope.ServiceProvider.GetRequiredService<IAuctionAssetsSender>();
                    await asyncWork(sender);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        public void Execute(Func<IBidSender, Task> asyncWork)
        {
            Task.Run(async () =>
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var sender = scope.ServiceProvider.GetRequiredService<IBidSender>();
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
